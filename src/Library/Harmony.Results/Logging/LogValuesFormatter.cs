using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Harmony.Results.Logging;

internal static class LogValuesFormatter
{
    private static MethodInfo? _formattedLogValuesToStringMethod;
    private static Func<string, object[], object>? _getFormattedLogValuesInstanceFunc;
    
    /// <summary>
    /// Replaces the arguments in the given string exactly like it is done in logging.
    /// It uses reflection to get a hold of <see cref="Microsoft.Extensions.Logging.FormattedLogValues"/> which is
    /// used internall by the logging system for this purpose. There is an issue to make this public in dotnet runtime,
    /// but hasn't yet been merged. https://github.com/dotnet/runtime/issues/67577
    /// </summary>
    /// <param name="message">The message template of the log</param>
    /// <param name="args">The arguments that need to be replaced in the message template</param>
    /// <returns>A string identical to the final log message</returns>
    internal static string ConvertLogMessageToString(string message, object[] args)
    {
        if (_formattedLogValuesToStringMethod is null || _getFormattedLogValuesInstanceFunc is null)
        {
            SetupLogValuesFormatter();
        }
        
        var formattedLogValuesInstance = _getFormattedLogValuesInstanceFunc!.Invoke(message, args);
        var result = (string)_formattedLogValuesToStringMethod!.Invoke(formattedLogValuesInstance, null)!;

        return result;
    }

    private static void SetupLogValuesFormatter()
    {
        var assembly = typeof(ILogger).Assembly;

        var formattedLogValuesType = assembly.GetType("Microsoft.Extensions.Logging.FormattedLogValues")!;
        
        _getFormattedLogValuesInstanceFunc = (message, values) =>
        {
            return Activator.CreateInstance(
                formattedLogValuesType,
                BindingFlags.Instance | BindingFlags.Public,
                null,
                new object[] { message, values },
                null)!;
        };
        
        MethodInfo toStringMethod = formattedLogValuesType.GetMethod(
            "ToString", 
            BindingFlags.Instance | BindingFlags.Public)!;

        _formattedLogValuesToStringMethod = toStringMethod;
    }
}