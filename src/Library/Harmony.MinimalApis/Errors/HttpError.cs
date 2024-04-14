using Harmony.Results.Abstractions;
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
    public List<ValidationInnerError> ValidationErrors { get; } = new();

    private readonly Action? _logAction;
    
    public HttpError(string errorCode, string description, int httpCode)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        _logAction = null;
    }
    
    public HttpError(string errorCode, string description, int httpCode, Action logAction)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        _logAction = logAction;
    }

    public HttpError(string errorCode, string description, int httpCode, List<ValidationInnerError> validationErrors)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        ValidationErrors = validationErrors;
        _logAction = null;
    }
    
    public HttpError(string errorCode, string description, int httpCode, List<ValidationInnerError> validationErrors, 
        Action logAction)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        ValidationErrors = validationErrors;
        _logAction = logAction;
    }
    
    public void Log()
    {
        _logAction?.Invoke();
    }
}