using Harmony.EntityFrameworkCore.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.EntityFrameworkCore;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public IRepository<TEntity> CreateRepository<TEntity>() 
        where TEntity : class, IEntity
    {
        return _serviceProvider.GetRequiredService<IRepository<TEntity>>();
    }
}
