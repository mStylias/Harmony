using System.Reflection;
using Harmony.Core.Abstractions.Factories;
using Harmony.Core.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddHarmony(this IServiceCollection services, Assembly assembly)
    {
        var assemblyTypes = assembly.GetTypes();
        
        var queryTypes = assemblyTypes
            .Where(t => t.BaseType is { IsGenericType: true } && 
                        t.BaseType.GetGenericTypeDefinition() == typeof(Query<,,>));
        
        foreach (var type in queryTypes)
        {
            services.AddTransient(type);
        }
        
        var commandTypes = assemblyTypes
            .Where(t => t.BaseType is { IsGenericType: true } && 
                        t.BaseType.GetGenericTypeDefinition() == typeof(Command<,>));
        
        foreach (var type in commandTypes)
        {
            services.AddTransient(type);
        }
        
        services.AddSingleton<ICohesionFabricator, CohesionFabricator>();
        
        return services;
    } 
}