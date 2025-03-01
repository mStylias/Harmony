using Harmony.Cqrs.Operations;

namespace Harmony.Cqrs.Abstractions;

public interface IOperationsManager
{
    /// <summary>
    /// Resolves the operation of the given type from the DI container.
    /// </summary>
    /// <typeparam name="TOperation">The type of operation you want to create.</typeparam>
    TOperation CreateOperation<TOperation>() 
        where TOperation : HarmonyOperation;

    // TODO: Add XML comments
    TOperation CreateOperation<TOperation>(Action<TOperation> configure)
        where TOperation : class, IOperationBase;
    
    /// <summary>
    /// Executes the given void operation.
    /// </summary>
    void ExecuteOperation(HarmonyOperation operation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the given operation and returns the output.
    /// </summary>
    /// <typeparam name="TOutput">The type of the operation output.</typeparam>
    TOutput ExecuteOperation<TOutput>(
        HarmonyOperation<TOutput> operation,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the given async operation and returns an awaitable Task.
    /// </summary>
    Task ExecuteOperationAsync(HarmonyOperation operation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the given operation asynchronously and returns an awaitable Task that returns the output.
    /// </summary>
    /// <typeparam name="TOutput">The type of the operation output.</typeparam>
    Task<TOutput> ExecuteOperationAsync<TOutput>(
        HarmonyOperation<TOutput> operation,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Undoes the given operation if it implements the <see cref="IReversible"/> interface.
    /// </summary>
    void UndoOperation(IReversible operation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Undoes the given operation if it implements the <see cref="IReversible"/> interface and returns the output that
    /// the undoing of the operation produces.
    /// </summary>
    /// <typeparam name="TResult">The type of result that the undoing of the operation produces.</typeparam>
    TResult UndoOperation<TResult>(
        IReversible<TResult> operation,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously undoes the given operation if it implements the <see cref="IReversible"/> interface.
    /// </summary>
    Task UndoOperationAsync(IAsyncReversible operation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously undoes the given operation if it implements the <see cref="IReversible"/> interface and returns the output that
    /// the undoing of the operation produces.
    /// </summary>
    /// <typeparam name="TResult">The type of result that the undoing of the operation produces.</typeparam>
    Task<TResult> UndoOperationAsync<TResult>(
        IAsyncReversible<TResult> operation,
        CancellationToken cancellationToken = default);
}