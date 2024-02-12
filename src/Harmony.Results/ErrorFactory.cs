using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Harmony.Results;

public static class ErrorFactory
{
    internal static ILogger Logger { get; set; } = null!;
    
    public static Error CreateError(string mainErrorCode, string description)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(mainErrorCode, description);
        return error;
    }

    public static Error CreateError(string mainErrorCode, string description, int httpStatusCode)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(mainErrorCode, description, httpStatusCode);
        return error;
    }

    public static Error CreateError(string mainErrorCode, string description, Action<ILogger> addLogging)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(mainErrorCode, description, addLogging);
        return error;
    }

    public static Error CreateError(string mainErrorCode, string description, int httpStatusCode, Action<ILogger> addLogging)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(mainErrorCode, description, httpStatusCode, addLogging);
        return error;
    }

    public static Error CreateError(string mainErrorCode, List<InnerError> innerErrors)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(mainErrorCode, innerErrors);
        return error;
    }

    public static Error CreateError(string mainErrorCode, List<InnerError> innerErrors, int httpStatusCode)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(mainErrorCode, innerErrors, httpStatusCode);
        return error;
    }

    public static Error CreateError(string mainErrorCode, List<InnerError> innerErrors, Action<ILogger> addLogging)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(mainErrorCode, innerErrors, addLogging);
        return error;
    }

    public static Error CreateError(string mainErrorCode, List<InnerError> innerErrors, int httpStatusCode, 
        Action<ILogger> addLogging)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(mainErrorCode, innerErrors, httpStatusCode, addLogging);
        return error;
    }
}