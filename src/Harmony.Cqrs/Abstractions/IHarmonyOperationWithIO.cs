namespace Harmony.Cqrs.Abstractions;

public interface IHarmonyOperationWithIO<TInput, TOutput> : IHarmonyOperationWithInput<TInput>
{
    TOutput Execute(CancellationToken cancellationToken = default);
    Task<TOutput> ExecuteAsync(CancellationToken cancellationToken = default);
}