using Harmony.Cqrs;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHarmonyCqrs(typeof(DependencyInjectionExtensions).Assembly);
        
        return services;
    }
}