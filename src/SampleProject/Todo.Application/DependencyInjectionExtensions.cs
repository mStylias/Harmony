using Harmony.Cqrs;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHarmony(typeof(DependencyInjectionExtensions).Assembly);
        
        return services;
    }
}