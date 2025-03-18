using Harmony.EntityFrameworkCore.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Harmony.EntityFrameworkCore;

public class TransactionManager : ITransactionManager
{
    private readonly DatabaseFacade _dbFacade;
    
    public TransactionManager(DbContext dbContext)
    {
        _dbFacade = dbContext.Database;
    }
    
    public IDbContextTransaction? CurrentTransaction => _dbFacade.CurrentTransaction;

    public AutoTransactionBehavior AutoTransactionBehavior
    {
        get => _dbFacade.AutoTransactionBehavior; 
        set => _dbFacade.AutoTransactionBehavior = value;
    }

    public bool AutoSavepointsEnabled
    {
        get => _dbFacade.AutoSavepointsEnabled;
        set => _dbFacade.AutoSavepointsEnabled = value;
    }
    
    public IDbContextTransaction BeginTransaction()
    {
        return _dbFacade.BeginTransaction();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return _dbFacade.BeginTransactionAsync(cancellationToken);
    }

    public void CommitTransaction()
    {
        _dbFacade.CommitTransaction();
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        return _dbFacade.CommitTransactionAsync(cancellationToken);
    }

    public void RollbackTransaction()
    {
        _dbFacade.RollbackTransaction();
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        return _dbFacade.RollbackTransactionAsync(cancellationToken);
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return _dbFacade.CreateExecutionStrategy();
    }
}