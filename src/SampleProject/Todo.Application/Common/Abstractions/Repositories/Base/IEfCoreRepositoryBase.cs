namespace Todo.Application.Common.Abstractions.Repositories.Base;

public interface IEfCoreRepositoryBase
{
    Task StartTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransaction();
}