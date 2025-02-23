namespace Harmony.Results;

public class Success
{
    private readonly Action? _logAction;
    
    public Success(Action logAction)
    {
        this._logAction = logAction;
    }
    
    public Success(string message)
    {
        this.Message = message;
    }
    
    public Success(string message, Action logAction)
    {
        this.Message = message;
        this._logAction = logAction;
    }

    internal Success()
    {
    }
    
    public string? Message { get; init; }
    
    public void Log()
    {
        this._logAction?.Invoke();
    }
}