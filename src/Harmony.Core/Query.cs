namespace Harmony.Core;

public abstract class Query : IRequest
{
    public bool UseTransaction { get; set; }
    public bool DoValidation { get; set; }
    
    public virtual void Execute()
    {
        throw new NotImplementedException();
    }
    
    public virtual T Execute<T>()
    {
        throw new NotImplementedException();
    }

    public virtual Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    public virtual Task<T> ExecuteAsync<T>(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}