using System.Diagnostics;
using Harmony.Results.Enums;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Harmony.Results.Logging;

namespace Harmony.Results.ErrorTypes;

public class ValidationError : LoggableHarmonyErrorImpl<ValidationError>
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
    
    public ValidationError PrependErrorCode()
    {
        Debug.Assert(LogAction is null, "Cannot modify the log message if logging is configured with a log action. " +
                                        "Use the InitializeLogMessage and append methods to build an error message instead");
        PrependLogMessage("{ErrorCode}: ", ErrorCode);
        return this;
    }
}