using Harmony.Results.Abstractions;
using Harmony.Results.Enums;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Harmony.Results.Logging;

namespace Harmony.Results.ErrorTypes;

public class ValidationError : LoggableHarmonyError
{
    public string ErrorCode { get; }
    public string Description { get; }
    public List<ValidationInnerError> InnerErrors { get; }

    public ValidationError(string errorCode, string description, List<ValidationInnerError> innerErrors, 
        Severity severity = Severity.Error) : base(severity)
    {
        ErrorCode = errorCode;
        Description = description;
        InnerErrors = innerErrors;
    }
    
    public ValidationError(string errorCode, string description, List<ValidationInnerError> innerErrors, 
        Action logAction, Severity severity = Severity.Error) : base(severity)
    {
        ErrorCode = errorCode;
        Description = description;
        InnerErrors = innerErrors;
        UseLogAction(logAction);
    }
}