﻿using System.Diagnostics;
using Harmony.Results.Enums;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Harmony.Results.Logging;

namespace Harmony.MinimalApis.Errors;

/// <summary>
/// Represents an http error that can be mapped to a problem of type Microsoft.AspNetCore.Http.IResult
/// </summary>
public class HttpError : LoggableHarmonyErrorImpl<HttpError>
{
    public string ErrorCode { get; }
    public string Description { get; }
    public int HttpCode { get; }
    public List<ValidationInnerError>? ValidationErrors { get; }
    
    public HttpError(string errorCode, string description, int httpCode, Severity severity = Severity.Error) 
        : base(severity)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        ValidationErrors = null;
    }
    
    public HttpError(string errorCode, string description, int httpCode, Action logAction, 
        Severity severity = Severity.Error) : base(severity)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        ValidationErrors = null;
        UseLogAction(logAction);
    }

    public HttpError(string errorCode, string description, int httpCode, List<ValidationInnerError> validationErrors, 
        Severity severity = Severity.Error) : base(severity)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        ValidationErrors = validationErrors;
    }
    
    public HttpError(string errorCode, string description, int httpCode, List<ValidationInnerError> validationErrors, 
        Action logAction, Severity severity = Severity.Error) : base(severity)
    {
        ErrorCode = errorCode;
        Description = description;
        HttpCode = httpCode;
        ValidationErrors = validationErrors;
        UseLogAction(logAction);
    }
    
    public HttpError PrependErrorCodeToLog()
    {
        Debug.Assert(GetLogBuilder() is not null, "Cannot modify the log message if logging " +
          "is configured with a log action or not configured at all. Use the InitializeLogMessage and Append " +
          "methods to build an error message instead");
        
        PrependLogMessage("{ErrorCode}: ", ErrorCode);
        return this;
    }
}