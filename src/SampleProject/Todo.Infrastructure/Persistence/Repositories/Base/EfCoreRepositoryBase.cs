using Todo.Application.Common.Abstractions.Repositories.Base;

namespace Todo.Infrastructure.Persistence.Repositories.Base;

public class EfCoreRepositoryBase : IEfCoreRepositoryBase
{
    private readonly AuthDbContext _dbContext;

    public EfCoreRepositoryBase(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task StartTransactionAsync()
    {
        await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);
    }

    public async Task CommitTransactionAsync()
    {
        await _dbContext.Database.CommitTransactionAsync().ConfigureAwait(false);
    }

    public async Task RollbackTransaction()
    {
        await _dbContext.Database.RollbackTransactionAsync().ConfigureAwait(false);
    }
}