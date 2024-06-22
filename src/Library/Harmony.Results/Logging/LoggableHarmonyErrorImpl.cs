using System.Diagnostics;
using Harmony.Results.Abstractions;
using Harmony.Results.Enums;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Harmony.Results.Logging;

public class LoggableHarmonyErrorImpl<TError> : ILoggableHarmonyError<TError>
    where TError : class, ILoggableHarmonyError<TError>
{
    private LogBuilder? _logBuilder;
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
        _logBuilder = new LogBuilder(logger, logLevel);
        return (this as TError)!;
    }
    
    public TError InitializeLogMessage(ILogger logger, LogLevel logLevel, [StructuredMessageTemplate] string message)
    {
        _logBuilder = new LogBuilder(logger, logLevel, message);
        return (this as TError)!;
    }
    
    public TError InitializeLogMessage(ILogger logger, LogLevel logLevel, [StructuredMessageTemplate] string message, 
        params object[] args)
    {
        _logBuilder = new LogBuilder(logger, logLevel, message, args);
        return (this as TError)!;
    }
    
    public TError InitializeLogMessage(ILogger logger, LogLevel logLevel, EventId eventId)
    {
        _logBuilder = new LogBuilder(logger, logLevel, eventId);
        return (this as TError)!;
    }
    
    /// <summary>
    /// Sets the exception that will be logged when the log method is called.
    /// </summary>
    public TError SetLogException(Exception exception)
    {
        Debug.Assert(_logBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        _logBuilder.SetException(exception);
        return (this as TError)!;
    }

    /// <summary>
    /// Adds the specified message at the end of the log message.
    /// </summary>
    /// <param name="message">The message to add</param>
    public TError AppendLogMessage([StructuredMessageTemplate] string message)
    {
        Debug.Assert(_logBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        _logBuilder.AppendLogMessage(message);
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
        Debug.Assert(_logBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        _logBuilder.AppendLogMessage(message, args);
        return (this as TError)!;
    }

    /// <summary>
    /// Adds the specified message at the start of the log message.
    /// </summary>
    /// <param name="message">The message to add</param>
    public TError PrependLogMessage([StructuredMessageTemplate] string message)
    {
        Debug.Assert(_logBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        _logBuilder.PrependLogMessage(message);
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
        Debug.Assert(_logBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        _logBuilder.PrependLogMessage(message, args);
        return (this as TError)!;
    }

    /// <summary>
    /// Logs the message of the log action if it was provided
    /// or the aggregated log if the log message was built gradually
    /// </summary>
    public void Log()
    {
        LogAction?.Invoke();
        _logBuilder?.Log();
    }

    public override string ToString()
    {
        if (_logBuilder is null)
        {
            return base.ToString() ?? string.Empty;
        }

        return _logBuilder.ToString();
    }
    
}