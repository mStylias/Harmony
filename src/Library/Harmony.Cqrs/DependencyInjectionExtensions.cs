using System.Reflection;
using Harmony.Cqrs.Abstractions;
using Harmony.Cqrs.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Cqrs;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds all the required harmony services including all the commands, queries and validators
    /// in the specified assembly. If you want to use scoped services for the operations, set the useScopeFactory to true
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <param name="useScopeFactory"></param>
    public static IServiceCollection AddHarmony(this IServiceCollection services, Assembly assembly, 
        bool useScopeFactory = false)
    {
        OperationFactory.UseScopeFactory = useScopeFactory;
        
        var assemblyTypes = assembly.GetTypes();
        
        var queryTypes = GetQueryTypes(assemblyTypes);
        var commandTypes = GetCommandTypes(assemblyTypes);
        
        services
            .AddHarmonyOperations(queryTypes, commandTypes)
            .AddHarmonyOperationValidators(assemblyTypes);

        if (useScopeFactory)
        {
            services.AddSingleton<IOperationFactory, OperationFactory>();
        }
        else
        {
            services.AddScoped<IOperationFactory, OperationFactory>();
        }
        
        return services;
    }

    private static Type[] GetQueryTypes(Type[] assemblyTypes)
    {
        var queryTypes = assemblyTypes
            .Where(t => 
                IsSubclassOfRawGeneric(typeof(Query), t) ||
                IsSubclassOfRawGeneric(typeof(Query<>), t) ||
                IsSubclassOfRawGeneric(typeof(Query<,>), t))
            .ToArray();

        return queryTypes;
    }
    
    private static Type[] GetCommandTypes(Type[] assemblyTypes)
    {
        var commandTypes = assemblyTypes
            .Where(t => 
                IsSubclassOfRawGeneric(typeof(Command), t) ||
                IsSubclassOfRawGeneric(typeof(Command<>), t) ||
                IsSubclassOfRawGeneric(typeof(Command<,>), t))
            .ToArray();

        return commandTypes;
    }
    
    private static IServiceCollection AddHarmonyOperations(this IServiceCollection services, Type[] queryTypes, 
        Type[] commandTypes)
    {
        foreach (var type in queryTypes)
        {
            services.AddScoped(type);
        }
        
        foreach (var type in commandTypes)
        {
            services.AddScoped(type);
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
    
    private static IServiceCollection AddHarmonyOperationValidators(this IServiceCollection services, 
        Type[] assemblyTypes)
    {
        var validatorTypes = assemblyTypes
            .Where(t => 
                t is { IsClass: true, IsAbstract: false } && 
                t.GetInterfaces().Any(IsHarmonyOperationValidatorInterface))
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
        return typeDefinition == typeof(IOperationValidator<,>);
    }
}