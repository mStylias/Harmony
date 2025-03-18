using System.Reflection;
using Harmony.EntityFrameworkCore.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.EntityFrameworkCore;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddHarmonyEfCore<TDbContext>(
        this IServiceCollection services, 
        Assembly entitiesAssembly,
        ServiceLifetime repositoriesLifetime = ServiceLifetime.Scoped)
        where TDbContext : DbContext
    {
        var assemblyTypes = entitiesAssembly.GetTypes();
        var entityTypes = assemblyTypes
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IEntity).IsAssignableFrom(t));

        foreach (var entityType in entityTypes)
        {
            var repositoryInterfaceType = typeof(IRepository<>).MakeGenericType(entityType);
            var repositoryImplementationType = typeof(Repository<>).MakeGenericType(entityType);
            
            switch (repositoriesLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(repositoryInterfaceType, serviceProvider =>
                    {
                        var dbContext = serviceProvider.GetRequiredService<TDbContext>();
                        return Activator.CreateInstance(repositoryImplementationType, dbContext)!;
                    });
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(repositoryInterfaceType, serviceProvider =>
                    {
                        var dbContext = serviceProvider.GetRequiredService<TDbContext>();
                        return Activator.CreateInstance(repositoryImplementationType, dbContext)!;
                    });
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(repositoryInterfaceType, serviceProvider =>
                    {
                        var dbContext = serviceProvider.GetRequiredService<TDbContext>();
                        return Activator.CreateInstance(repositoryImplementationType, dbContext)!;
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(repositoriesLifetime), repositoriesLifetime, null);
            }
        }

        return services;
    }
}