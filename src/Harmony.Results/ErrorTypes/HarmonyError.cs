using Harmony.Results.Abstractions;

namespace Harmony.Results.ErrorTypes;

public readonly record struct HarmonyError : ILoggableHarmonyError
{
    public string ErrorCode { get; }
    public string Description { get; }
    public string? ErrorType { get; }
    
    private readonly Action? _logAction;
    
    public HarmonyError(string errorCode, string description)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = null;
        _logAction = null;
    }

    public HarmonyError(string errorCode, string description, string errorType)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = errorType;
        _logAction = null;
    }

    public HarmonyError(string errorCode, string description, Action logAction)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = null;
        _logAction = logAction;
    }

    public HarmonyError(string errorCode, string description, string errorType, Action logAction)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = errorType;
        _logAction = logAction;
    }

    public void Log()
    {
        _logAction?.Invoke();
    }
}