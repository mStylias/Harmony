using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.EntityFrameworkCore;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddHarmonyEfCore(
        this IServiceCollection services, 
        ServiceLifetime repositoriesLifetime, 
        Assembly entitiesAssembly)
    {
        var assemblyTypes = entitiesAssembly.GetTypes();
        var entityTypes = assemblyTypes
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IEntity).IsAssignableFrom(t));

        foreach (var entityType in entityTypes)
        {
            var repositoryInterfaceType = typeof(IRepository<>).MakeGenericType(entityType);
            var repositoryImplementationType = typeof(Repository<>).MakeGenericType(entityType);

            // Register the interface and implementation with the given lifetime
            services.Add(new ServiceDescriptor(repositoryInterfaceType, repositoryImplementationType, repositoriesLifetime));
        }

        return services;
    }
}