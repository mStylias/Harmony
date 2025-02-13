﻿using JetBrains.Annotations;
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
    
    internal Exception? Exception;

    private bool _includeLogLevelInToString = true;
    private string _message = string.Empty;
    private readonly List<object> _args = new();
    
    public LogBuilder(ILogger logger, LogLevel logLevel)
    {
        _logger = logger;
        _logLevel = logLevel;
    }
    
    public LogBuilder(ILogger logger, LogLevel logLevel, [StructuredMessageTemplate] string message)
    {
        _logger = logger;
        _logLevel = logLevel;
        _message = message;
    }
    
    public LogBuilder(ILogger logger, LogLevel logLevel, [StructuredMessageTemplate] string message, 
        params object[] args)
    {
        _logger = logger;
        _logLevel = logLevel;
        _message = message;
        _args.AddRange(args);
    }
    
    public LogBuilder(ILogger logger, LogLevel logLevel, EventId eventId)
    {
        _logger = logger;
        _logLevel = logLevel;
        _eventId = eventId;
    }

    public void IncludeLogLevelInToString(bool value)
    {
        _includeLogLevelInToString = value;
    }
    
    public void SetException(Exception exception)
    {
        Exception = exception;
    }
    
    public void AppendLogMessage([StructuredMessageTemplate] string message)
    {
        if (_message.Length == 0)
        {
            _message = message;
            return;
        }
        
        _message = _message.Insert(_message.Length, message);
    }
    
    public void AppendLogMessage([StructuredMessageTemplate] string message, params object[] args)
    {
        AppendLogMessage(message);
        _args.AddRange(args);
    }
    
    public void PrependLogMessage([StructuredMessageTemplate] string message)
    {
        if (_message.Length == 0)
        {
            _message = message;
            return;
        }
        
        _message = _message.Insert(0, message);
    }
    
    public void PrependLogMessage([StructuredMessageTemplate] string message, params object[] args)
    {
        PrependLogMessage(message);
        
        for (int i = args.Length - 1; i >= 0; i--)
        {
            _args.Insert(0, args[i]);
        }
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
            _logger.Log(_logLevel, Exception, _message, parameters);
            return;
        }
        
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.Log(_logLevel, _eventId.Value, Exception, _message, parameters);
    }

    public override string ToString()
    {
        var startingMessage = _includeLogLevelInToString 
            ? $"[{_logLevel}]: " 
            : string.Empty;

        // If there are no arguments, simply return the message.
        if (_args.Count == 0)
        {
            return startingMessage + _message;
        }
        
        // Format the message with the arguments.
        var formattedLogValues = LogValuesFormatter.ConvertLogMessageToString(_message, _args.ToArray());

        return startingMessage + formattedLogValues;
    }
}