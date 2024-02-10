using System.Reflection;
using Harmony.MinimalApis.Structure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.MinimalApis;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApiEndpoints(this IServiceCollection services, Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsClass == false || type.IsAbstract) continue;
            
            var interfaces = type.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (@interface != typeof(IEndpoint)) continue;
                
                services.AddTransient(@interface, type);
            }
        }

        return services;
    }
    
    // TODO: Add support for versioning
    public static void MapApiEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetServices<IEndpoint>();
        
        foreach (var endpoint in endpoints)
        {
            var routeHandlerBuilder = endpoint.AddEndpoint(app);
            routeHandlerBuilder.WithTags(endpoint.Tag);
        }
    }
}