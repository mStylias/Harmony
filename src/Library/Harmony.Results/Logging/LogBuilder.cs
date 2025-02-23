using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Harmony.Results.Logging;

/// <summary>
/// Provides a way to gradually build a log message and log it at the end, while retaining
/// the best practices of logging.
/// </summary>
public class LogBuilder
{
    private readonly ILogger _logger;
    private readonly LogLevel _logLevel;
    private readonly EventId? _eventId;
    private readonly List<object> _args = new();

    private bool _includeLogLevelInToString = true;
    private string _message = string.Empty;

    public LogBuilder(ILogger logger, LogLevel logLevel)
    {
        this._logger = logger;
        this._logLevel = logLevel;
    }
    
    public LogBuilder(ILogger logger, LogLevel logLevel, [StructuredMessageTemplate] string message)
    {
        this._logger = logger;
        this._logLevel = logLevel;
        this._message = message;
    }
    
    public LogBuilder(ILogger logger, LogLevel logLevel, [StructuredMessageTemplate] string message, params object[] args)
    {
        this._logger = logger;
        this._logLevel = logLevel;
        this._message = message;
        this._args.AddRange(args);
    }
    
    public LogBuilder(ILogger logger, LogLevel logLevel, EventId eventId)
    {
        this._logger = logger;
        this._logLevel = logLevel;
        this._eventId = eventId;
    }
    
    internal Exception? Exception { get; set; }
    
    public void IncludeLogLevelInToString(bool value)
    {
        this._includeLogLevelInToString = value;
    }
    
    public void SetException(Exception exception)
    {
        this.Exception = exception;
    }
    
    public void AppendLogMessage([StructuredMessageTemplate] string message)
    {
        if (this._message.Length == 0)
        {
            this._message = message;
            return;
        }
        
        this._message = this._message.Insert(this._message.Length, message);
    }
    
    public void AppendLogMessage([StructuredMessageTemplate] string message, params object[] args)
    {
        this.AppendLogMessage(message);
        this._args.AddRange(args);
    }
    
    public void PrependLogMessage([StructuredMessageTemplate] string message)
    {
        if (this._message.Length == 0)
        {
            this._message = message;
            return;
        }
        
        this._message = this._message.Insert(0, message);
    }
    
    public void PrependLogMessage([StructuredMessageTemplate] string message, params object[] args)
    {
        this.PrependLogMessage(message);
        
        for (int i = args.Length - 1; i >= 0; i--)
        {
            this._args.Insert(0, args[i]);
        }
    }
    
#pragma warning disable CA2254
    public void Log()
    {
        if (string.IsNullOrEmpty(this._message))
        {
            return;
        }
        
        object?[] parameters = this._args.ToArray();

        if (this._eventId is null)
        {
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            this._logger.Log(this._logLevel, this.Exception, this._message, parameters);

            return;
        }
        
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        this._logger.Log(this._logLevel, this._eventId.Value, this.Exception, this._message, parameters);
    }
#pragma warning restore CA2254
    
    public override string ToString()
    {
        var startingMessage = this._includeLogLevelInToString 
            ? $"[{this._logLevel}]: " 
            : string.Empty;

        var exceptionContent = string.Empty;
        if (this.Exception is not null)
        {
            exceptionContent = $"{Environment.NewLine} Exception message: {this.Exception.Message} + Environment.NewLine" +
                               $"Stack trace: {this.Exception.StackTrace}";
        }
        
        // If there are no arguments, simply return the message.
        if (this._args.Count == 0)
        {
            return startingMessage + this._message + exceptionContent;
        }
        
        // Format the message with the arguments.
        var formattedLogValues = LogValuesFormatter.ConvertLogMessageToString(this._message, this._args.ToArray());

        return startingMessage + formattedLogValues + exceptionContent;
    }
}