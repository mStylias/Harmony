using System.Diagnostics;
using System.Text.Json.Serialization;
using Harmony.Results.Abstractions;
using Harmony.Results.Enums;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Harmony.Results.Logging;

/// <summary>
/// Provides the base class for an error that can be logged using the log builder.
/// </summary>
/// <typeparam name="TError">The developer defined error type.</typeparam>
public class LoggableHarmonyErrorCore<TError> : ILoggableHarmonyError<TError>
    where TError : class, ILoggableHarmonyError<TError>
{
    protected LoggableHarmonyErrorCore(Severity severity)
    {
        this.Severity = severity;
    }
    
    public Severity Severity { get; }
    
    [JsonIgnore]
    public Exception? LogException => this.LogBuilder?.Exception;
    
    internal Action? LogAction { get; set; }
    
    protected LogBuilder? LogBuilder { get; private set; }
    
    public TError InitializeLogMessage(ILogger logger, LogLevel logLevel)
    {
        this.LogBuilder = new LogBuilder(logger, logLevel);
        return (this as TError)!;
    }
    
    public TError InitializeLogMessage(ILogger logger, LogLevel logLevel, [StructuredMessageTemplate] string message)
    {
        this.LogBuilder = new LogBuilder(logger, logLevel, message);
        return (this as TError)!;
    }
    
    public TError InitializeLogMessage(
        ILogger logger, 
        LogLevel logLevel, 
        [StructuredMessageTemplate] string message, 
        params object[] args)
    {
        this.LogBuilder = new LogBuilder(logger, logLevel, message, args);
        return (this as TError)!;
    }
    
    public TError InitializeLogMessage(ILogger logger, LogLevel logLevel, EventId eventId)
    {
        this.LogBuilder = new LogBuilder(logger, logLevel, eventId);
        return (this as TError)!;
    }
    
    /// <summary>
    /// Sets the exception that will be logged when the log method is called.
    /// </summary>
    public TError SetLogException(Exception exception)
    {
        Debug.Assert(this.LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        this.LogBuilder.SetException(exception);
        return (this as TError)!;
    }

    /// <summary>
    /// Adds the specified message at the end of the log message.
    /// </summary>
    /// <param name="message">The message to add.</param>
    public TError AppendLogMessage([StructuredMessageTemplate] string message)
    {
        Debug.Assert(this.LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        this.LogBuilder.AppendLogMessage(message);
        return (this as TError)!;
    }

    /// <summary>
    /// Adds the specified message at the end of the log message.
    /// </summary>
    /// <param name="message">The message to add.</param>
    /// <param name="args">The arguments that should replace the placeholders in the message.
    /// Exactly like the normal logging works in .NET.</param>
    public TError AppendLogMessage([StructuredMessageTemplate] string message, params object[] args)
    {
        Debug.Assert(this.LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        this.LogBuilder.AppendLogMessage(message, args);
        return (this as TError)!;
    }

    /// <summary>
    /// Adds the specified message at the start of the log message.
    /// </summary>
    /// <param name="message">The message to add.</param>
    public TError PrependLogMessage([StructuredMessageTemplate] string message)
    {
        Debug.Assert(this.LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        this.LogBuilder.PrependLogMessage(message);
        return (this as TError)!;
    }

    /// <summary>
    /// Adds the specified message at the start of the log message.
    /// </summary>
    /// <param name="message">The message to add.</param>
    /// <param name="args">The arguments that should replace the placeholders in the message.
    /// Exactly like the normal logging works in .NET.</param>
    public TError PrependLogMessage([StructuredMessageTemplate] string message, params object[] args)
    {
        Debug.Assert(this.LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        this.LogBuilder.PrependLogMessage(message, args);
        return (this as TError)!;
    }

    public TError IncludeLogLevelInToString(bool value)
    {
        Debug.Assert(this.LogBuilder is not null, "InitializeLogMessage must be called before any other log building method");
        this.LogBuilder.IncludeLogLevelInToString(value);
        return (this as TError)!;
    }
    
    /// <summary>
    /// Logs the message of the log action and the log builder if they are not null.
    /// </summary>
    public void Log()
    {
        this.LogAction?.Invoke();
        this.LogBuilder?.Log();
    }
    
    public override string ToString()
    {
        if (this.LogBuilder is null)
        {
            return base.ToString() ?? string.Empty;
        }

        return this.LogBuilder.ToString();
    }

    /// <inheritdoc/>
    public void OverrideLogBuilderWith<TErrorType>(LoggableHarmonyErrorCore<TErrorType> harmonyError)
        where TErrorType : class, ILoggableHarmonyError<TErrorType>
    {
        this.LogBuilder = harmonyError.LogBuilder;
    }
    
    protected void UseLogAction(Action logAction)
    {
        this.LogAction = logAction;
    }
}