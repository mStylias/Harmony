using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Harmony.Results;

public static class ErrorFactory
{
    internal static ILogger Logger { get; set; } = null!;
    
    public static Error CreateError(string code, string description)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(code, description);
        return error;
    }

    public static Error CreateError(string code, string description, int httpStatusCode)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(code, description, httpStatusCode);
        return error;
    }

    public static Error CreateError(string code, string description, Action<ILogger> addLogging)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(code, description, addLogging);
        return error;
    }

    public static Error CreateError(string code, string description, int httpStatusCode, Action<ILogger> addLogging)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(code, description, httpStatusCode, addLogging);
        return error;
    }

    public static Error CreateError(List<InnerError> innerErrors)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(innerErrors);
        return error;
    }

    public static Error CreateError(List<InnerError> innerErrors, int httpStatusCode)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(innerErrors, httpStatusCode);
        return error;
    }

    public static Error CreateError(List<InnerError> innerErrors, Action<ILogger> addLogging)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(innerErrors, addLogging);
        return error;
    }

    public static Error CreateError(List<InnerError> innerErrors, int httpStatusCode, Action<ILogger> addLogging)
    {
        Debug.Assert(Logger is not null);
        var error = new Error(Logger);
        error.Populate(innerErrors, httpStatusCode, addLogging);
        return error;
    }
}