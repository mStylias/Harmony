using System.Diagnostics.CodeAnalysis;
using Harmony.Cqrs.Abstractions;
using Harmony.Cqrs.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Cqrs;

[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Reviewed.")]
public class OperationsManager : IOperationsManager
{
    private readonly IServiceProvider _serviceProvider;
    
    public OperationsManager(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider;
    }
    
    public TOperation CreateOperation<TOperation>()
        where TOperation : HarmonyOperation
    {
        return _serviceProvider.GetRequiredService<TOperation>();
    }

    public TOperation CreateOperation<TOperation>(Action<TOperation> configure)
        where TOperation : class, IOperationBase
    {
        var operation = _serviceProvider.GetRequiredService<TOperation>();
        configure(operation);
        return operation;
    }

    public void ExecuteOperation(HarmonyOperation operation, CancellationToken cancellationToken = default)
    {
        operation.Execute(cancellationToken);
    }

    public TOutput ExecuteOperation<TOutput>(
        HarmonyOperation<TOutput> operation, 
        CancellationToken cancellationToken = default)
    {
        return operation.Execute(cancellationToken);
    }

    public Task ExecuteOperationAsync(HarmonyOperation operation, CancellationToken cancellationToken = default)
    {
        return operation.ExecuteAsync(cancellationToken);
    }

    public Task<TOutput> ExecuteOperationAsync<TOutput>(
        HarmonyOperation<TOutput> operation, 
        CancellationToken cancellationToken = default)
    {
        return operation.ExecuteAsync(cancellationToken);
    }

    public void UndoOperation(IReversible operation, CancellationToken cancellationToken = default)
    {
        operation.Undo(cancellationToken);
    }

    public TResult UndoOperation<TResult>(IReversible<TResult> operation, CancellationToken cancellationToken = default)
    {
        return operation.Undo(cancellationToken);
    }

    public Task UndoOperationAsync(IAsyncReversible operation, CancellationToken cancellationToken = default)
    {
        return operation.UndoAsync(cancellationToken);
    }

    public Task<TResult> UndoOperationAsync<TResult>(
        IAsyncReversible<TResult> operation, 
        CancellationToken cancellationToken = default)
    {
        return operation.UndoAsync(cancellationToken);
    }
}