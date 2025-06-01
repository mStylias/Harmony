using Harmony.EntityFrameworkCore.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Harmony.EntityFrameworkCore;

public class DatabaseManager : IDatabaseManager
{
    private readonly DbContext _dbContext;

    public DatabaseManager(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc/>
    public string? ProviderName => _dbContext.Database.ProviderName;
    
    /// <inheritdoc/>
    public ChangeTracker ChangeTracker => _dbContext.ChangeTracker;
    
    /// <inheritdoc/>
    public IModel Model => _dbContext.Model;
    
    /// <inheritdoc/>
    public DbContextId ContextId => _dbContext.ContextId;
    
    /// <inheritdoc/>
    public bool EnsureCreated()
    {
        return _dbContext.Database.EnsureCreated();
    }

    /// <inheritdoc/>
    public Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public bool EnsureDeleted()
    {
        return _dbContext.Database.EnsureDeleted();
    }

    /// <inheritdoc/>
    public Task<bool> EnsureDeletedAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Database.EnsureDeletedAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public bool CanConnect()
    {
        return _dbContext.Database.CanConnect();
    }

    /// <inheritdoc/>
    public Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Database.CanConnectAsync(cancellationToken);
    }
}