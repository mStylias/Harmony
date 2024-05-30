using Harmony.Results.Abstractions;
using Harmony.Results.Enums;
using Harmony.Results.ErrorTypes.InnerErrorTypes;

namespace Harmony.Results.ErrorTypes;

public readonly record struct ValidationError : ILoggableHarmonyError
{
    public string ErrorCode { get; }
    public string Description { get; }
    public List<ValidationInnerError> InnerErrors { get; }
    
    public Severity Severity { get; }
    
    private readonly Action? _logAction;

    public ValidationError(string errorCode, string description, List<ValidationInnerError> innerErrors, 
        Severity severity = Severity.Error)
    {
        ErrorCode = errorCode;
        Description = description;
        InnerErrors = innerErrors;
        _logAction = null;
        Severity = severity;
    }
    
    public ValidationError(string errorCode, string description, List<ValidationInnerError> innerErrors, 
        Action logAction, Severity severity = Severity.Error)
    {
        ErrorCode = errorCode;
        Description = description;
        InnerErrors = innerErrors;
        _logAction = logAction;
        Severity = severity;
    }

    public void Log()
    {
        _logAction?.Invoke();
    }
}