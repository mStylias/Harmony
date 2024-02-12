using Harmony.Results.Abstractions;
using Microsoft.Extensions.Logging;

namespace Harmony.Results;

public class Error : IError
{
    public string MainErrorCode { get; internal set; }
    public int HttpStatusCode { get; private set; }
    public List<InnerError> InnerErrors { get; private set; } = null!;
    
    private Action<ILogger>? _logAction;
    private readonly ILogger _logger;

    internal Error(ILogger logger)
    {
        _logger = logger;
    }
    
    public void Log()
    {
        _logAction?.Invoke(_logger);
    }
    
    // Single error construction
    internal Error Populate(string code, string description)
    {
        MainErrorCode = code;
        InnerErrors = new()
        {
            new InnerError(code, description)
        };
        return this;
    }
    
    internal Error Populate(string code, string description, int httpStatusCode)
    {
        MainErrorCode = code;
        InnerErrors = new()
        {
            new InnerError(code, description)
        };
        HttpStatusCode = httpStatusCode;
        return this;
    }
    
    // Single error construction with logging
    internal Error Populate(string code, string description, Action<ILogger> addLogging)
    {
        MainErrorCode = code;
        InnerErrors = new()
        {
            new InnerError(code, description)
        };
        _logAction = addLogging;
        return this;
    }
    
    internal Error Populate(string code, string description, int httpStatusCode, Action<ILogger> addLogging)
    {
        MainErrorCode = code;
        InnerErrors = new()
        {
            new InnerError(code, description)
        };
        HttpStatusCode = httpStatusCode;
        _logAction = addLogging;
        return this;
    }
    
    // Multiple errors construction
    internal Error Populate(string code, List<InnerError> innerErrors)
    {
        MainErrorCode = code;
        InnerErrors = innerErrors;
        return this;
    }
    
    internal Error Populate(string code, List<InnerError> innerErrors, int httpStatusCode)
    {
        MainErrorCode = code;
        InnerErrors = innerErrors;
        HttpStatusCode = httpStatusCode;
        return this;
    }
    
    // Multiple errors construction with logging
    internal Error Populate(string code, List<InnerError> innerErrors, Action<ILogger> addLogging)
    {
        MainErrorCode = code;
        InnerErrors = innerErrors;
        _logAction = addLogging;
        return this;
    }
    
    internal Error Populate(string code, List<InnerError> innerErrors, int httpStatusCode, Action<ILogger> addLogging)
    {
        MainErrorCode = code;
        InnerErrors = innerErrors;
        HttpStatusCode = httpStatusCode;
        _logAction = addLogging;
        return this;
    }
}