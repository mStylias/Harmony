using Harmony.Core.Abstractions;

namespace Harmony.Core;

public abstract class Command<TInput, TOutput> : IOperationWithIO<TInput, TOutput>
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

public abstract class Command<TOutput> : IHarmonyOperation
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

public abstract class Command : IHarmonyOperation
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
