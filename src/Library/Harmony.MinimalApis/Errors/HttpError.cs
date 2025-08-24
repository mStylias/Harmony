using System.Diagnostics;
using Harmony.Results.Enums;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Harmony.Results.Logging;

namespace Harmony.MinimalApis.Errors;

/// <summary>
/// Represents an http error that can be mapped to a problem of type Microsoft.AspNetCore.Http.IResult.
/// </summary>
public class HttpError : LoggableHarmonyErrorCore<HttpError>
{
    public HttpError(string errorCode, string description, int httpCode, Severity severity = Severity.Error) 
        : base(severity)
    {
        this.ErrorCode = errorCode;
        this.Description = description;
        this.HttpCode = httpCode;
        this.ValidationErrors = null;
    }
    
    public HttpError(
        string errorCode, 
        string description, 
        int httpCode, 
        Action logAction, 
        Severity severity = Severity.Error)
        : base(severity)
    {
        this.ErrorCode = errorCode;
        this.Description = description;
        this.HttpCode = httpCode;
        this.ValidationErrors = null;
        this.UseLogAction(logAction);
    }

    public HttpError(
        string errorCode, 
        string description, 
        int httpCode, 
        ICollection<ValidationInnerError> validationErrors, 
        Severity severity = Severity.Error) 
        : base(severity)
    {
        this.ErrorCode = errorCode;
        this.Description = description;
        this.HttpCode = httpCode;
        this.ValidationErrors = validationErrors;
    }
    
    public HttpError(
        string errorCode, 
        string description, 
        int httpCode, 
        ICollection<ValidationInnerError> validationErrors,
        Action logAction, 
        Severity severity = Severity.Error) 
        : base(severity)
    {
        this.ErrorCode = errorCode;
        this.Description = description;
        this.HttpCode = httpCode;
        this.ValidationErrors = validationErrors;
        this.UseLogAction(logAction);
    }
    
    public string ErrorCode { get; }

    public string Description { get; }

    public int HttpCode { get; }

    public ICollection<ValidationInnerError>? ValidationErrors { get; }

    public override void Log()
    {
        base.Log();
        
        if (this.ValidationErrors is not null && this.ValidationErrors.Count > 0)
        {
            foreach (var validationError in this.ValidationErrors)
            {
                validationError.Log();
            }
        }
    }
    
    public HttpError PrependErrorCodeToLog()
    {
        const string assertionMessage = "Cannot modify the log message if logging " +
            "is configured with a log action or not configured at all. Use the InitializeLogMessage and Append " +
            "methods to build an error message instead";
        
        Debug.Assert(this.LogBuilder is not null, assertionMessage);
        
        this.PrependLogMessage("{ErrorCode}: ", this.ErrorCode);
        return this;
    }
}