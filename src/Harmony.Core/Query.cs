using Harmony.Core.Abstractions;

namespace Harmony.Core;

public abstract class Query<TInput, TOutput, TConfiguration> : IOperationWithInputOutput<TInput, TOutput, TConfiguration>
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

public abstract class Query<TInput, TOutput> : IOperationWithInputOutput<TInput, TOutput>
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

public abstract class Query<TConfiguration> : IHarmonyOperation<TConfiguration>
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
