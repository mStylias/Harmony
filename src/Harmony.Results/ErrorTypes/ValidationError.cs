using Harmony.Results.Abstractions;
using Harmony.Results.ErrorTypes.InnerErrorTypes;

namespace Harmony.Results.ErrorTypes;

public readonly record struct ValidationError : ILoggableHarmonyError
{
    public string ErrorCode { get; }
    public string Description { get; }
    public List<ValidationInnerError> InnerErrors { get; }
    
    private readonly Action? _logAction;

    public ValidationError(string errorCode, string description, List<ValidationInnerError> innerErrors)
    {
        ErrorCode = errorCode;
        Description = description;
        InnerErrors = innerErrors;
        _logAction = null;
    }
    
    public ValidationError(string errorCode, string description, List<ValidationInnerError> innerErrors, 
        Action logAction)
    {
        ErrorCode = errorCode;
        Description = description;
        InnerErrors = innerErrors;
        _logAction = logAction;
    }

    public void Log()
    {
        _logAction?.Invoke();
    }
}