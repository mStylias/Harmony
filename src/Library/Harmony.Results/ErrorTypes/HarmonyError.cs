using System.Diagnostics;
using Harmony.Results.Enums;
using Harmony.Results.Logging;

namespace Harmony.Results.ErrorTypes;

/// <summary>
/// An implementation of an error that can be used with the Result type. Because it extends from
/// LoggableHarmonyErrorImpl it can be logged using the log builder
/// </summary>
public class HarmonyError : LoggableHarmonyErrorImpl<HarmonyError>
{
    /// <summary>
    /// The code that represents the error
    /// </summary>
    public string ErrorCode { get; }
    
    /// <summary>
    /// A human-readable description of the error
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// The type of the error that can be used to categorize the error
    /// </summary>
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
    
    /// <summary>
    /// Prepends the error code to the log message if logging is configured using the log building methods provided by
    /// <see cref="LoggableHarmonyErrorImpl"/> and NOT the log action. If the log action is used instead, this method
    /// throws an error.
    /// </summary>
    /// <returns>This instance of the HarmonyError with the error code prepended to the configured log</returns>
    public HarmonyError PrependErrorCodeToLog()
    {
        Debug.Assert(LogBuilder is not null, 
    "Cannot modify the log message if logging is not configured with the log building methods. " +
             "Use the InitializeLogMessage and append methods to build an error message in order to use this method");
        PrependLogMessage("{ErrorCode}: ", ErrorCode);
        return this;
    }
}