namespace Harmony.Results;

public class Success
{
    public string? Message { get; init; }
    private readonly Action? _logAction;
    
    public Success(Action logAction)
    {
        _logAction = logAction;
    }
    
    public Success(string message)
    {
        Message = message;
    }
    
    public Success(string message, Action logAction)
    {
        Message = message;
        _logAction = logAction;
    }
    
    internal Success() {}
    
    public void Log()
    {
        _logAction?.Invoke();
    }
}

public class Success<TMetadata>
{
    public string? Message { get; init; }
    public TMetadata Metadata { get; init; }
    private readonly Action? _logAction;

    public Success(TMetadata metadata)
    {
        Metadata = metadata;
    }
    public Success(Action logAction, TMetadata metadata)
    {
        _logAction = logAction;
        Metadata = metadata;
    }
    
    public Success(string message, TMetadata metadata)
    {
        Message = message;
        Metadata = metadata;
    }

    public Success(string message, Action logAction, TMetadata metadata)
    {
        Message = message;
        Metadata = metadata;
        _logAction = logAction;
    }
    
    public void Log()
    {
        _logAction?.Invoke();
    }
}