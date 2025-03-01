using Harmony.Cqrs.Abstractions;

namespace Harmony.Cqrs.Operations;

public abstract class HarmonyOperation : IHarmonyOperation
{
    public virtual void Execute(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public abstract class HarmonyOperation<TOutput> : IHarmonyOperation<TOutput>
{
    public virtual TOutput Execute(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TOutput> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}