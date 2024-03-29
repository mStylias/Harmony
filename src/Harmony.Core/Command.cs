using Harmony.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Core;

public abstract class Command<TInput, TOutput> : IHarmonyOperationWithIO<TInput, TOutput>
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
    
    public IServiceScope? Scope { get; set; }
    public void Dispose()
    {
        if (Scope is null) return;
        
        Scope.Dispose();
        GC.SuppressFinalize(this);
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
    
    public IServiceScope? Scope { get; set; }
    public void Dispose()
    {
        if (Scope is null) return;
        
        Scope.Dispose();
        GC.SuppressFinalize(this);
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

    public IServiceScope? Scope { get; set; }
    public void Dispose()
    {
        if (Scope is null) return;
        
        Scope.Dispose();
        GC.SuppressFinalize(this);
    }
}
