using Harmony.Core.Abstractions;

namespace Harmony.Core;

public abstract class Command<TInput, TOutput, TConfiguration> : IOperationWithInputOutput<TInput, TOutput, TConfiguration>
{
    public TConfiguration? Configuration { get; set;  }
    public virtual TOutput Execute(TInput input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public abstract class Command<TInput, TOutput> : IOperationWithInputOutput<TInput, TOutput>
{
    public virtual TOutput Execute(TInput input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public abstract class Command<TConfiguration> : IHarmonyOperation<TConfiguration>
{
    public TConfiguration? Configuration { get; set; }
    
    public virtual void Execute(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task ExecuteAsync(CancellationToken cancellationToken = default)
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
