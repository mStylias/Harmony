namespace Todo.Application.Common.Abstractions.Repositories;

public interface IEfCoreRepositoryBase
{
    Task StartTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransaction();
}