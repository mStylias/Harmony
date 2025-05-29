namespace Harmony.EntityFrameworkCore.Abstractions;

public interface IRepositoryFactory
{
    public IRepository<TEntity> CreateRepository<TEntity>()
        where TEntity : class, IEntity;
}