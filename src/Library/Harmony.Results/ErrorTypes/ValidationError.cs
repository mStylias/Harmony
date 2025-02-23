using System.Diagnostics;
using Harmony.Results.Enums;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Harmony.Results.Logging;

namespace Harmony.Results.ErrorTypes;

/// <summary>
/// An error type implementation that is used when a validation error occurs. This error type can be used
/// to return multiple inner errors in order to provide more information about the error that occured.
/// </summary>
public class ValidationError : LoggableHarmonyErrorCore<ValidationError>
{
    public ValidationError(
        string errorCode, 
        string description, 
        ICollection<ValidationInnerError> innerErrors, 
        Severity severity = Severity.Error) 
        : base(severity)
    {
        this.ErrorCode = errorCode;
        this.Description = description;
        this.InnerErrors = innerErrors;
    }
    
    public ValidationError(
        string errorCode, 
        string description, 
        ICollection<ValidationInnerError> innerErrors, 
        Action logAction, 
        Severity severity = Severity.Error) 
        : base(severity)
    {
        this.ErrorCode = errorCode;
        this.Description = description;
        this.InnerErrors = innerErrors;
        this.UseLogAction(logAction);
    }
    
    public string ErrorCode { get; }
    
    public string Description { get; }
    
    public ICollection<ValidationInnerError> InnerErrors { get; }
    
    public ValidationError PrependErrorCodeToLog()
    {
        Debug.Assert(
            this.LogAction is null, 
            "Cannot modify the log message if logging is configured with a log action. " +
                "Use the InitializeLogMessage and append methods to build an error message instead");
        
        this.PrependLogMessage("{ErrorCode}: ", this.ErrorCode);
        return this;
    }
}