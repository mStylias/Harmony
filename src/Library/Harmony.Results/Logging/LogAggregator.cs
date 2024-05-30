using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Harmony.Results.Logging;

/// <summary>
/// Provides a way to gradually build a log message and log it at the end, while retaining
/// the best practices of logging.
/// </summary>
public class LogAggregator
{
    private readonly ILogger _logger;
    private readonly LogLevel _logLevel;
    private readonly EventId? _eventId;
    
    private Exception? _exception;

    private string _message = string.Empty;
    private readonly List<object> _args = new();
    
    public LogAggregator(ILogger logger, LogLevel logLevel)
    {
        _logger = logger;
        _logLevel = logLevel;
    }
    
    public LogAggregator(ILogger logger, LogLevel logLevel, EventId eventId)
    {
        _logger = logger;
        _logLevel = logLevel;
        _eventId = eventId;
    }
    
    public void SetException(Exception exception)
    {
        _exception = exception;
    }
    
    public void AppendLogMessage([StructuredMessageTemplate] string message)
    {
        _message = _message.Insert(_message.Length - 1, message);
    }
    
    public void AppendLogMessage([StructuredMessageTemplate] string message, params object[] args)
    {
        AppendLogMessage(message);
        _args.AddRange(args);
    }
    
    public void PrependLogMessage([StructuredMessageTemplate] string message)
    {
        _message = _message.Insert(0, message);
    }
    
    public void PrependLogMessage([StructuredMessageTemplate] string message, params object[] args)
    {
        PrependLogMessage(message);
        _args.AddRange(args);
    }

    public void Log()
    {
        if (_message == string.Empty)
        {
            return;
        }
        
        object?[] parameters = _args.ToArray();

        if (_eventId is null)
        {
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.Log(_logLevel, _exception, _message, parameters);
            return;
        }
        
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.Log(_logLevel, _eventId.Value, _exception, _message, parameters);
    }
}