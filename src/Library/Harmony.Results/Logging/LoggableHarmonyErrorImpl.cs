using System.Diagnostics;
using Harmony.Results.Abstractions;
using Harmony.Results.Enums;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Harmony.Results.Logging;

/// <summary>
/// Provides the base class for an error that can be logged using the log builder
/// </summary>
/// <typeparam name="TError"></typeparam>
public class LoggableHarmonyErrorImpl<TError> : ILoggableHarmonyError<TError>
    where TError : class, ILoggableHarmonyError<TError>
{
    protected LogBuilder? LogBuilder;
    protected Action? LogAction;

    public LoggableHarmonyErrorImpl(Severity severity)
    {
        Severity = severity;
    }

    public Severity Severity { get; }
    
    protected void UseLogAction(Action logAction)
    {
        LogAction = logAction;
    }
    
    public TError InitializeLogMessage(ILogger logger, LogLevel logLevel)
    {
        LogBuilder = new LogBuilder(logger, logLevel);
        return (this as TError)!;
    }
    
    public TError InitializeLogMessage(ILogger logger, LogLevel logLevel, [StructuredMessageTemplate] string message)
    {
        LogBuilder = new LogBuilder(logger, logLevel, message);
        return (this as TError)!;
    }
    
    public TError InitializeLogMessage(ILogger logger, LogLevel logLevel, [StructuredMessageTemplate] string message, 
        params object[] args)
    {
        LogBuilder = new LogBuilder(logger, logLevel, message, args);
        return (this as TError)!;
    }
    
    public TError InitializeLogMessage(ILogger logger, LogLevel logLevel, EventId eventId)
    {
        LogBuilder = new LogBuilder(logger, logLevel, eventId);
        return (this as TError)!;
    }
    
    /// <summary>
    /// Sets the exception that will be logged when the log method is called.
    /// </summary>
    public TError SetLogException(Exception exception)
    {
        Debug.Assert(LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        LogBuilder.SetException(exception);
        return (this as TError)!;
    }

    /// <summary>
    /// Adds the specified message at the end of the log message.
    /// </summary>
    /// <param name="message">The message to add</param>
    public TError AppendLogMessage([StructuredMessageTemplate] string message)
    {
        Debug.Assert(LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        LogBuilder.AppendLogMessage(message);
        return (this as TError)!;
    }

    /// <summary>
    /// Adds the specified message at the end of the log message.
    /// </summary>
    /// <param name="message">The message to add</param>
    /// <param name="args">The arguments that should replace the placeholders in the message.
    /// Exactly like the normal logging works in .NET</param>
    public TError AppendLogMessage([StructuredMessageTemplate] string message, params object[] args)
    {
        Debug.Assert(LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        LogBuilder.AppendLogMessage(message, args);
        return (this as TError)!;
    }

    /// <summary>
    /// Adds the specified message at the start of the log message.
    /// </summary>
    /// <param name="message">The message to add</param>
    public TError PrependLogMessage([StructuredMessageTemplate] string message)
    {
        Debug.Assert(LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        LogBuilder.PrependLogMessage(message);
        return (this as TError)!;
    }

    /// <summary>
    /// Adds the specified message at the start of the log message.
    /// </summary>
    /// <param name="message">The message to add</param>
    /// <param name="args">The arguments that should replace the placeholders in the message.
    /// Exactly like the normal logging works in .NET</param>
    public TError PrependLogMessage([StructuredMessageTemplate] string message, params object[] args)
    {
        Debug.Assert(LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        LogBuilder.PrependLogMessage(message, args);
        return (this as TError)!;
    }

    public TError IncludeLogLevelInToString(bool value)
    {
        Debug.Assert(LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        LogBuilder.IncludeLogLevelInToString(value);
        return (this as TError)!;
    }
    
    /// <summary>
    /// Logs the message of the log action and the log builder if they are not null
    /// </summary>
    public void Log()
    {
        LogAction?.Invoke();
        LogBuilder?.Log();
    }
    
    public override string ToString()
    {
        if (LogBuilder is null)
        {
            return base.ToString() ?? string.Empty;
        }

        return LogBuilder.ToString();
    }
    
}