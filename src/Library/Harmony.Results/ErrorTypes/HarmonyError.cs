using Harmony.Results.Abstractions;
using Harmony.Results.Enums;
using Harmony.Results.Logging;

namespace Harmony.Results.ErrorTypes;

public class HarmonyError : LoggableHarmonyError
{
    public string ErrorCode { get; }
    public string Description { get; }
    public string? ErrorType { get; }
    
    public HarmonyError(string errorCode, string description, Severity severity = Severity.Error) : base(severity)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = null;
    }

    public HarmonyError(string errorCode, string description, string errorType, Severity severity = Severity.Error) 
        : base(severity)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = errorType;
    }

    public HarmonyError(string errorCode, string description, Action logAction, Severity severity = Severity.Error) 
        : base(severity)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = null;
        UseLogAction(logAction);
    }

    public HarmonyError(string errorCode, string description, string errorType, Action logAction, 
        Severity severity = Severity.Error) : base(severity)
    {
        ErrorCode = errorCode;
        Description = description;
        ErrorType = errorType;
        UseLogAction(logAction);
    }
}