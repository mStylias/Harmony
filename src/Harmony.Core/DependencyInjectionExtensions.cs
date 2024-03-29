using System.Reflection;
using Harmony.Core.Abstractions;
using Harmony.Core.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Core;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds all the required harmony services including all the commands, queries and validators
    /// in the specified assembly. If you want to use scoped services for the operations, set the useScope to true
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <param name="useScope"></param>
    /// <returns></returns>
    public static IServiceCollection AddHarmony(this IServiceCollection services, Assembly assembly, 
        bool useScope = false)
    {
        services
            .AddHarmonyOperations(assembly)
            .AddHarmonyOperationValidators(assembly);

        OperationFactory.UseScope = useScope;
        services.AddSingleton<IOperationFactory, OperationFactory>();
        
        return services;
    }

    private static IServiceCollection AddHarmonyOperations(this IServiceCollection services, Assembly assembly)
    {
        var assemblyTypes = assembly.GetTypes();
        
        var queryTypes = assemblyTypes
            .Where(t => IsSubclassOfRawGeneric(typeof(Query), t) ||
                        IsSubclassOfRawGeneric(typeof(Query<>), t) ||
                        IsSubclassOfRawGeneric(typeof(Query<,>), t));
        
        foreach (var type in queryTypes)
        {
            if (OperationFactory.UseScope)
            {
                services.AddScoped(type);
            }
            else
            {
                services.AddTransient(type);
            }
        }
        
        var commandTypes = assemblyTypes
            .Where(t => IsSubclassOfRawGeneric(typeof(Command), t) ||
                        IsSubclassOfRawGeneric(typeof(Command<>), t) ||
                        IsSubclassOfRawGeneric(typeof(Command<,>), t));

        foreach (var type in commandTypes)
        {
            if (OperationFactory.UseScope)
            {
                services.AddScoped(type);
            }
            else
            {
                services.AddTransient(type);
            }
        }
        
        return services;
    }
    
    private static bool IsSubclassOfRawGeneric(Type generic, Type? toCheck)
    {
        while (toCheck != null && toCheck != typeof(object))
        {
            var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
            if (generic == cur)
            {
                return true;
            }
            toCheck = toCheck.BaseType;
        }
        return false;
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
        return typeDefinition == typeof(IHarmonyOperationValidator<,>);
    }
}