using System.Reflection;
using Harmony.Core.Abstractions.Factories;
using Harmony.Core.Factories;
using Harmony.Core.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddHarmony(this IServiceCollection services, Assembly assembly)
    {
        services
            .AddHarmonyOperations(assembly)
            .AddHarmonyOperationValidators(assembly);

        services.AddSingleton<ICohesionFabricator, CohesionFabricator>();
        return services;
    }

    private static IServiceCollection AddHarmonyOperations(this IServiceCollection services, Assembly assembly)
    {
        var assemblyTypes = assembly.GetTypes();
        
        var queryTypes = assemblyTypes
            .Where(t => t.BaseType is { IsGenericType: true } && (
                        t.BaseType.GetGenericTypeDefinition() == typeof(Query<,,>) ||
                        t.BaseType.GetGenericTypeDefinition() == typeof(Query<,>) ||
                        t.BaseType.GetGenericTypeDefinition() == typeof(Query<>) ||
                        t.BaseType.GetGenericTypeDefinition() == typeof(Query)));
        
        foreach (var type in queryTypes)
        {
            services.AddTransient(type);
        }
        
        var commandTypes = assemblyTypes
            .Where(t => t.BaseType is { IsGenericType: true } && (
                        t.BaseType.GetGenericTypeDefinition() == typeof(Command<,,>) ||
                        t.BaseType.GetGenericTypeDefinition() == typeof(Command<,>) ||
                        t.BaseType.GetGenericTypeDefinition() == typeof(Command<>) ||
                        t.BaseType.GetGenericTypeDefinition() == typeof(Command)));
        
        foreach (var type in commandTypes)
        {
            services.AddTransient(type);
        }
        
        return services;
    }
    
    private static IServiceCollection AddHarmonyOperationValidators(this IServiceCollection services, Assembly assembly)
    {
        var validatorTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(IsHarmonyOperationValidatorInterface))
            .ToList();

        foreach (var type in validatorTypes)
        {
            var interfaceType = type.GetInterfaces().First(IsHarmonyOperationValidatorInterface);
            services.AddTransient(interfaceType, type);
        }

        return services;
    }
    
    private static bool IsHarmonyOperationValidatorInterface(Type type)
    {
        if (!type.IsGenericType)
            return false;

        var typeDefinition = type.GetGenericTypeDefinition();
        return typeDefinition == typeof(IHarmonyOperationValidator<>);
    }
}