using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Cqrs.Abstractions;

public interface IHarmonyOperation : IDisposable
{
    public IServiceScope? Scope { get; set; }
    public void Undo(CancellationToken cancellationToken);
    public Task UndoAsync(CancellationToken cancellationToken);
    TResult Undo<TResult>(CancellationToken cancellationToken);
    Task<TResult> UndoAsync<TResult>(CancellationToken cancellationToken);

    public TMetadata GetMetadata<TMetadata>(IWithMetadata<TMetadata> operationWithMetadata) 
        where TMetadata : class
    {
        return operationWithMetadata.Metadata;
    }
}