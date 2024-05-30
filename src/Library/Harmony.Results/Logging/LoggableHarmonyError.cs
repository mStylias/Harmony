using System.Diagnostics;
using Harmony.Results.Abstractions;
using Harmony.Results.Enums;
using Microsoft.Extensions.Logging;

namespace Harmony.Results.Logging;

public class LoggableHarmonyError : ILoggableHarmonyError
{
    private LogAggregator? _logAggregator;
    private Action? _logAction;

    public LoggableHarmonyError(Severity severity)
    {
        Severity = severity;
    }

    public Severity Severity { get; }
    
    protected void UseLogAction(Action logAction)
    {
        _logAction = logAction;
    }
    
    public ILoggableHarmonyError InitializeLogMessage(ILogger logger, LogLevel logLevel)
    {
        _logAggregator = new LogAggregator(logger, logLevel);
        return this;
    }
    
    public ILoggableHarmonyError InitializeLogMessage(ILogger logger, LogLevel logLevel, EventId eventId)
    {
        _logAggregator = new LogAggregator(logger, logLevel, eventId);
        return this;
    }
    
    /// <summary>
    /// Sets the exception that will be logged when the log method is called.
    /// </summary>
    public ILoggableHarmonyError SetLogException(Exception exception)
    {
        Debug.Assert(_logAggregator is not null, "InitializeLogMessage must be called before any other method");
        _logAggregator.SetException(exception);
        return this;
    }

    /// <summary>
    /// Adds the specified message at the end of the log message.
    /// </summary>
    /// <param name="message">The message to add</param>
    public ILoggableHarmonyError AppendLogMessage(string message)
    {
        Debug.Assert(_logAggregator is not null, "InitializeLogMessage must be called before any other method");
        _logAggregator.AppendLogMessage(message);
        return this;
    }

    /// <summary>
    /// Adds the specified message at the end of the log message.
    /// </summary>
    /// <param name="message">The message to add</param>
    /// <param name="args">The arguments that should replace the placeholders in the message.
    /// Exactly like the normal logging works in .NET</param>
    public ILoggableHarmonyError AppendLogMessage(string message, params object[] args)
    {
        Debug.Assert(_logAggregator is not null, "InitializeLogMessage must be called before any other method");
        _logAggregator.AppendLogMessage(message, args);
        return this;
    }

    /// <summary>
    /// Adds the specified message at the start of the log message.
    /// </summary>
    /// <param name="message">The message to add</param>
    public ILoggableHarmonyError PrependLogMessage(string message)
    {
        Debug.Assert(_logAggregator is not null, "InitializeLogMessage must be called before any other method");
        _logAggregator.PrependLogMessage(message);
        return this;
    }

    /// <summary>
    /// Adds the specified message at the start of the log message.
    /// </summary>
    /// <param name="message">The message to add</param>
    /// <param name="args">The arguments that should replace the placeholders in the message.
    /// Exactly like the normal logging works in .NET</param>
    public ILoggableHarmonyError PrependLogMessage(string message, params object[] args)
    {
        Debug.Assert(_logAggregator is not null, "InitializeLogMessage must be called before any other method");
        _logAggregator.PrependLogMessage(message, args);
        return this;
    }

    /// <summary>
    /// Logs the message of the log action if it was provided
    /// or the aggregated log if the log message was built gradually
    /// </summary>
    public void Log()
    {
        if (_logAction is not null)
        {
            _logAction.Invoke();
            return;
        }
        
        Debug.Assert(_logAggregator is not null, "InitializeLogMessage must be called before any other method");
        _logAggregator.Log();
    }
}