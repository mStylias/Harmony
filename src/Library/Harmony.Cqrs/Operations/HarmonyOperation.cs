using Harmony.Cqrs.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Cqrs.Operations;

public abstract class HarmonyOperation<TInput, TOutput> : IHarmonyOperationWithIO<TInput, TOutput>
{
    public abstract TInput? Input { get; set; }
    public IServiceScope? Scope { get; set; }

    public virtual TOutput Execute(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TOutput> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual void Undo(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task UndoAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual TResult Undo<TResult>(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TResult> UndoAsync<TResult>(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Scope?.Dispose();
        }
    }
}

public abstract class HarmonyOperation<TOutput> : IHarmonyOperation
{
    public IServiceScope? Scope { get; set; }

    public virtual TOutput Execute(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TOutput> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual void Undo(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task UndoAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual TResult Undo<TResult>(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TResult> UndoAsync<TResult>(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Scope?.Dispose();
        }
    }
}

public abstract class HarmonyOperation : IHarmonyOperation
{
    public IServiceScope? Scope { get; set; }

    public virtual void Execute(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual void Undo(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task UndoAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual TResult Undo<TResult>(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TResult> UndoAsync<TResult>(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Scope?.Dispose();
        }
    }
}

