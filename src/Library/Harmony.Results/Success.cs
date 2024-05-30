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