namespace Harmony.Cqrs.Abstractions;

public interface IHarmonyOperation : IOperationBase
{
    void Execute(CancellationToken cancellationToken = default);
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}

public interface IHarmonyOperation<TOutput> : IOperationBase
{
    TOutput Execute(CancellationToken cancellationToken = default);
    Task<TOutput> ExecuteAsync(CancellationToken cancellationToken = default);
}