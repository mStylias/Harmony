namespace Harmony.Core.Abstractions;

public interface IOperationWithIO<TInput, TOutput> : IOperationWithInput<TInput>
{
    TOutput Execute(CancellationToken cancellationToken = default);
    Task<TOutput> ExecuteAsync(CancellationToken cancellationToken = default);
}