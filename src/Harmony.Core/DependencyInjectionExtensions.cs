using System.Reflection;
using Harmony.Core.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddHarmony(this IServiceCollection services, Assembly assembly)
    {
        // Automatically discover and register all Command and query implementations
        var commandTypes = assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(typeof(Command)));
        
        var queryTypes = assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(typeof(Query)));

        foreach (var command in commandTypes)
        {
            services.AddTransient(command);
        }

        foreach (var query in queryTypes)
        {
            services.AddTransient(query);
        }

        services.AddSingleton<ICohesionFabricator, CohesionFabricator>();
        
        return services;
    } 
}