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
    
    public string? ProviderName => _dbContext.Database.ProviderName;
    public ChangeTracker ChangeTracker => _dbContext.ChangeTracker;
    public IModel Model => _dbContext.Model;
    public DbContextId ContextId => _dbContext.ContextId;
    public bool EnsureCreated()
    {
        return _dbContext.Database.EnsureCreated();
    }

    public Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }

    public bool EnsureDeleted()
    {
        return _dbContext.Database.EnsureDeleted();
    }

    public Task<bool> EnsureDeletedAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Database.EnsureDeletedAsync(cancellationToken);
    }

    public bool CanConnect()
    {
        return _dbContext.Database.CanConnect();
    }

    public Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Database.CanConnectAsync(cancellationToken);
    }
}