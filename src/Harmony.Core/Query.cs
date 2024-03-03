using Harmony.Core.Abstractions;

namespace Harmony.Core;

public abstract class Query<TInput, TOutput> : IOperationWithIO<TInput, TOutput>
{
    public abstract TInput? Input { get; set; }
    public virtual TOutput Execute(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TOutput> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public abstract class Query<TOutput> : IHarmonyOperation
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

public abstract class Query : IHarmonyOperation
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
