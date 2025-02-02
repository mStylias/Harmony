using Harmony.Cqrs.Abstractions;
using Microsoft.Extensions.DependencyInjection;

// Keeping this namespace to reduce breaking changes
// ReSharper disable once CheckNamespace
namespace Harmony.Cqrs;

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
        if (Scope is null) return;
        
        Scope.Dispose();
        GC.SuppressFinalize(this);
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
        if (Scope is null) return;
        
        Scope.Dispose();
        GC.SuppressFinalize(this);
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
        if (Scope is null) return;
        
        Scope.Dispose();
        GC.SuppressFinalize(this);
    }
}

