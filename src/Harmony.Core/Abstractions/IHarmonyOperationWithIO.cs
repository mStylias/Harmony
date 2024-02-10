namespace Harmony.Core.Abstractions;

public interface IOperationWithInputOutput<in TInput, TOutput, TConfiguration> : IHarmonyOperation<TConfiguration>
{
    TOutput Execute(TInput input, CancellationToken cancellationToken = default);
    Task<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}

public interface IOperationWithInputOutput<in TInput, TOutput> : IHarmonyOperation
{
    TOutput Execute(TInput input, CancellationToken cancellationToken = default);
    Task<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}