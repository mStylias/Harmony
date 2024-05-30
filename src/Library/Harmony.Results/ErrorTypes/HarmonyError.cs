using Harmony.Results.Abstractions;
using Harmony.Results.Enums;

namespace Harmony.Results.ErrorTypes;

public readonly record struct HarmonyError : ILoggableHarmonyError
{
    public string ErrorCode { get; }
    public string Description { get; }
    public string? ErrorType { get; }

    public Severity Severity { get; } = Severity.Error;
    
    private readonly Action? _logAction;
    
    public HarmonyError(string errorCode, string description, Severity severity = Severity.Error)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = null;
        _logAction = null;
        Severity = severity;
    }

    public HarmonyError(string errorCode, string description, string errorType, Severity severity = Severity.Error)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = errorType;
        _logAction = null;
        Severity = severity;
    }

    public HarmonyError(string errorCode, string description, Action logAction, Severity severity = Severity.Error)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = null;
        _logAction = logAction;
        Severity = severity;
    }

    public HarmonyError(string errorCode, string description, string errorType, Action logAction, Severity severity = Severity.Error)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = errorType;
        _logAction = logAction;
        Severity = severity;
    }

    public void Log()
    {
        _logAction?.Invoke();
    }
}