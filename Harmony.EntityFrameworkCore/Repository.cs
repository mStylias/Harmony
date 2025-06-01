using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Harmony.EntityFrameworkCore.Abstractions;
using Harmony.EntityFrameworkCore.Mapping.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.EntityFrameworkCore;

[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "To remain similar to the Microsoft version")]
public sealed class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;
    private readonly IServiceProvider _serviceProvider;

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
        _serviceProvider = ((IInfrastructure<IServiceProvider>)_dbSet).Instance;
    }

    public DbSet<TEntity> DbSet => _dbSet;

    /// <inheritdoc/>
    public IEntityType EntityType => _dbSet.EntityType;

    /// <inheritdoc/>
    public IAsyncEnumerable<TEntity> AsAsyncEnumerable() => (IAsyncEnumerable<TEntity>)_dbSet;
    
    /// <inheritdoc/>
    public IQueryable<TEntity> AsQueryable() => _dbSet;

    /// <inheritdoc/>
    public LocalView<TEntity> Local => _dbSet.Local;

    /// <inheritdoc/>
    public TEntity? Find(params object?[]? keyValues)
        => _dbSet.Find(keyValues);

    /// <inheritdoc/>
    public ValueTask<TEntity?> FindAsync(params object?[]? keyValues)
        => _dbSet.FindAsync(keyValues);

    /// <inheritdoc/>
    public ValueTask<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken)
        => _dbSet.FindAsync(keyValues, cancellationToken);

    /// <inheritdoc/>
    public EntityEntry<TEntity> Add(TEntity entity)
        => _dbSet.Add(entity);

    /// <inheritdoc/>
    public ValueTask<EntityEntry<TEntity>> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
        => _dbSet.AddAsync(entity, cancellationToken);

    /// <inheritdoc/>
    public EntityEntry<TEntity> Attach(TEntity entity)
        => _dbSet.Attach(entity);

    /// <inheritdoc/>
    public EntityEntry<TEntity> Remove(TEntity entity)
        => _dbSet.Remove(entity);

    /// <inheritdoc/>
    public EntityEntry<TEntity> Update(TEntity entity)
        => _dbSet.Update(entity);

    /// <inheritdoc/>
    public void AddRange(params TEntity[] entities)
        => _dbSet.AddRange(entities);

    /// <inheritdoc/>
    public Task AddRangeAsync(params TEntity[] entities)
        => _dbSet.AddRangeAsync(entities);

    /// <inheritdoc/>
    public void AttachRange(params TEntity[] entities)
        => _dbSet.AttachRange(entities);

    /// <inheritdoc/>
    public void RemoveRange(params TEntity[] entities)
        => _dbSet.RemoveRange(entities);

    /// <inheritdoc/>
    public void UpdateRange(params TEntity[] entities)
        => _dbSet.UpdateRange(entities);

    /// <inheritdoc/>
    public void AddRange(IEnumerable<TEntity> entities)
        => _dbSet.AddRange(entities);

    /// <inheritdoc/>
    public Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
        => _dbSet.AddRangeAsync(entities, cancellationToken);

    /// <inheritdoc/>
    public void AttachRange(IEnumerable<TEntity> entities)
        => _dbSet.AttachRange(entities);

    /// <inheritdoc/>
    public void RemoveRange(IEnumerable<TEntity> entities)
        => _dbSet.RemoveRange(entities);

    /// <inheritdoc/>
    public void UpdateRange(IEnumerable<TEntity> entities)
        => _dbSet.UpdateRange(entities);

    /// <inheritdoc/>
    public EntityEntry<TEntity> Entry(TEntity entity)
        => _dbSet.Entry(entity);

    /// <inheritdoc/>
    IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        => ((IEnumerable<TEntity>)_dbSet).GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)_dbSet).GetEnumerator();

    /// <inheritdoc/>
    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        => ((IAsyncEnumerable<TEntity>)_dbSet).GetAsyncEnumerator(cancellationToken);
    
    /// <inheritdoc/>
    Type IQueryable.ElementType
        => ((IQueryable)_dbSet).ElementType;

    /// <inheritdoc/>
    Expression IQueryable.Expression
        => ((IQueryable)_dbSet).Expression;

    /// <inheritdoc/>
    IQueryProvider IQueryable.Provider
        => ((IQueryable)_dbSet).Provider;

    /// <inheritdoc/>
    IServiceProvider IInfrastructure<IServiceProvider>.Instance
        => ((IInfrastructure<IServiceProvider>)_dbSet).Instance;

    /// <inheritdoc/>
    IList IListSource.GetList()
        => ((IListSource)_dbSet).GetList();

    /// <inheritdoc/>
    bool IListSource.ContainsListCollection
        => ((IListSource)_dbSet).ContainsListCollection;
    
    /// <inheritdoc/>
    public IEntityMapper<TEntity, TDto> GetMapperFor<TDto>()
    {
        var mapper = _serviceProvider.GetRequiredService<IEntityMapper<TEntity, TDto>>();
        return mapper;
    }

    /// <inheritdoc/>
    public IQueryable<TDto> ProjectTo<TDto>(IQueryable<TEntity> queryable)
    {
        // TODO: Test
        var mapper = GetMapperFor<TDto>();
        return mapper.ProjectToDto(queryable);
    }

    /// <inheritdoc/>
    public EntityEntry<TEntity> Add<TDto>(TDto dto)
    {
        // TODO: Test
        var mapper = GetMapperFor<TDto>();
        var entity = mapper.ToEntity(dto);
        return Add(entity);
    }

    /// <inheritdoc/>
    public ValueTask<EntityEntry<TEntity>> AddAsync<TDto>(TDto dto, CancellationToken cancellationToken = default)
    {
        var mapper = GetMapperFor<TDto>();
        var entity = mapper.ToEntity(dto);
        return AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public void AddRange<TDto>(params TDto[] dtos)
    {
        var mapper = GetMapperFor<TDto>();
        var entities = dtos.Select(dto => mapper.ToEntity(dto)).ToArray();
        AddRange(entities);
    }

    /// <inheritdoc/>
    public void AddRange<TDto>(IEnumerable<TDto> dtos)
    {
        var mapper = GetMapperFor<TDto>();
        var entities = dtos.Select(dto => mapper.ToEntity(dto)).ToArray();
        AddRange(entities);
    }

    /// <inheritdoc/>
    public Task AddRangeAsync<TDto>(params TDto[] dtos)
    {
        var mapper = GetMapperFor<TDto>();
        var entities = dtos.Select(dto => mapper.ToEntity(dto)).ToArray();
        return AddRangeAsync(entities);
    }

    /// <inheritdoc/>
    public Task AddRangeAsync<TDto>(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
    {
        var mapper = GetMapperFor<TDto>();
        var entities = dtos.Select(dto => mapper.ToEntity(dto)).ToArray();
        return AddRangeAsync(entities, cancellationToken);
    }

    /// <inheritdoc/>
    public EntityEntry<TEntity> Remove<TDto>(TDto dto)
    {
        var mapper = GetMapperFor<TDto>();
        var entity = mapper.ToEntity(dto);
        return Remove(entity);
    }

    /// <inheritdoc/>
    public int RemoveWhere(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet
            .Where(predicate)
            .ExecuteDelete();
    }
    
    /// <inheritdoc/>
    public Task RemoveWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _dbSet
            .Where(predicate)
            .ExecuteDeleteAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public EntityEntry<TEntity> Update<TDto>(TDto dto)
    {
        var mapper = GetMapperFor<TDto>();
        var entity = mapper.ToEntity(dto);
        return Update(entity);
    }

    /// <inheritdoc/>
    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }

    /// <inheritdoc/>
    public Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();   
    }
}