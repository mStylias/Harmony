using Harmony.Results.Abstractions;
using Harmony.Results.Enums;
using Harmony.Results.ErrorTypes.InnerErrorTypes;

namespace Harmony.MinimalApis.Errors;

/// <summary>
/// Represents an http error that can be mapped to a problem of type Microsoft.AspNetCore.Http.IResult
/// </summary>
public readonly record struct HttpError : ILoggableHarmonyError
{
    public string ErrorCode { get; }
    public string Description { get; }
    public int HttpCode { get; }
    public List<ValidationInnerError>? ValidationErrors { get; }
    
    public Severity Severity { get; }

    private readonly Action? _logAction;
    
    public HttpError(string errorCode, string description, int httpCode, Severity severity = Severity.Error)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        _logAction = null;
        ValidationErrors = null;
        Severity = severity;
    }
    
    public HttpError(string errorCode, string description, int httpCode, Action logAction, 
        Severity severity = Severity.Error)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        _logAction = logAction;
        ValidationErrors = null;
        Severity = severity;
    }

    public HttpError(string errorCode, string description, int httpCode, List<ValidationInnerError> validationErrors, 
        Severity severity = Severity.Error)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        ValidationErrors = validationErrors;
        _logAction = null;
        Severity = severity;
    }
    
    public HttpError(string errorCode, string description, int httpCode, List<ValidationInnerError> validationErrors, 
        Action logAction, Severity severity = Severity.Error)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        ValidationErrors = validationErrors;
        _logAction = logAction;
        Severity = severity;
    }
    
    public void Log()
    {
        _logAction?.Invoke();
    }
}