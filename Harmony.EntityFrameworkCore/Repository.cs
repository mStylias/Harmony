using Microsoft.EntityFrameworkCore;

namespace Harmony.EntityFrameworkCore;

public abstract class Repository<TEntity> : DbSet<TEntity>
    where TEntity : class, IEntity
{
}