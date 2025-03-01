namespace Harmony.Cqrs.Abstractions;

public interface IReversible : IOperationBase
{
    public void Undo(CancellationToken cancellationToken);
}

public interface IAsyncReversible : IOperationBase
{
    public Task UndoAsync(CancellationToken cancellationToken);
}

public interface IReversible<out TResult> : IOperationBase
{
    TResult Undo(CancellationToken cancellationToken);
}

public interface IAsyncReversible<TResult> : IOperationBase
{
    Task<TResult> UndoAsync(CancellationToken cancellationToken);
}