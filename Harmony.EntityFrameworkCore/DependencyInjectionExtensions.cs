using System.Diagnostics;
using System.Reflection;
using Harmony.EntityFrameworkCore.Abstractions;
using Harmony.EntityFrameworkCore.Mapping.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.EntityFrameworkCore;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddHarmonyEfCore<TDbContext>(
        this IServiceCollection services,
        Assembly entitiesAssembly,
        ServiceLifetime repositoriesLifetime = ServiceLifetime.Scoped,
        ServiceLifetime databaseManagerLifetime = ServiceLifetime.Scoped,
        ServiceLifetime transactionManagerLifetime = ServiceLifetime.Scoped)
        where TDbContext : DbContext
    {
        var assemblyTypes = entitiesAssembly.GetTypes();

        AddRepositories<TDbContext>(services, repositoriesLifetime, assemblyTypes);
        AddMappers(services, assemblyTypes);
        AddDatabaseManager<TDbContext>(services, databaseManagerLifetime);
        AddTransactionManager<TDbContext>(services, transactionManagerLifetime);

        return services;
    }

    private static void AddMappers(this IServiceCollection services, Type[] assemblyTypes)
    {
        var mapperInterfaceType = typeof(IEntityMapper<,>);
        
        foreach (var implementationType in assemblyTypes.Where(t => t.IsClass && !t.IsAbstract))
        {
            var implementedInterfaces = implementationType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == mapperInterfaceType);

            foreach (var interfaceType in implementedInterfaces)
            {
                Debug.WriteLine("Adding mapper: " + interfaceType.FullName + " -> " + implementationType.FullName);
                services.AddSingleton(interfaceType, implementationType);
            }
        }
    }

    private static void AddRepositories<TDbContext>(
        IServiceCollection services,
        ServiceLifetime repositoriesLifetime,
        Type[] assemblyTypes)
        where TDbContext : DbContext
    {
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
                        return Activator.CreateInstance(repositoryImplementationType, dbContext, serviceProvider)!;
                    });
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(repositoryInterfaceType, serviceProvider =>
                    {
                        var dbContext = serviceProvider.GetRequiredService<TDbContext>();
                        return Activator.CreateInstance(repositoryImplementationType, dbContext, serviceProvider)!;
                    });
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(repositoryInterfaceType, serviceProvider =>
                    {
                        var dbContext = serviceProvider.GetRequiredService<TDbContext>();
                        return Activator.CreateInstance(repositoryImplementationType, dbContext, serviceProvider)!;
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(repositoriesLifetime), repositoriesLifetime, null);
            }
        }
    }

    private static void AddDatabaseManager<TDbContext>(
        IServiceCollection services,
        ServiceLifetime lifetime)
        where TDbContext : DbContext
    {
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton<IDatabaseManager>(sp =>
                    new DatabaseManager(sp.GetRequiredService<TDbContext>()));
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped<IDatabaseManager>(sp =>
                    new DatabaseManager(sp.GetRequiredService<TDbContext>()));
                break;
            case ServiceLifetime.Transient:
                services.AddTransient<IDatabaseManager>(sp =>
                    new DatabaseManager(sp.GetRequiredService<TDbContext>()));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
        }
    }

    private static void AddTransactionManager<TDbContext>(
        IServiceCollection services,
        ServiceLifetime lifetime)
        where TDbContext : DbContext
    {
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton<ITransactionManager>(sp =>
                    new TransactionManager(sp.GetRequiredService<TDbContext>()));
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped<ITransactionManager>(sp =>
                    new TransactionManager(sp.GetRequiredService<TDbContext>()));
                break;
            case ServiceLifetime.Transient:
                services.AddTransient<ITransactionManager>(sp =>
                    new TransactionManager(sp.GetRequiredService<TDbContext>()));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
        }
    }
}