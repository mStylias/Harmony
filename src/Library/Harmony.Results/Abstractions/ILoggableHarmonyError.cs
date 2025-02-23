using Harmony.Results.Logging;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Harmony.Results.Abstractions;

public interface ILoggableHarmonyError<out TError> : IHarmonyError
    where TError : class, ILoggableHarmonyError<TError>
{
    TError InitializeLogMessage(ILogger logger, LogLevel logLevel);
    
    TError InitializeLogMessage(ILogger logger, LogLevel logLevel, [StructuredMessageTemplate] string message);
    
    TError InitializeLogMessage(
        ILogger logger, 
        LogLevel logLevel, 
        [StructuredMessageTemplate] string message, 
        params object[] args);
    
    TError InitializeLogMessage(ILogger logger, LogLevel logLevel, EventId eventId);
    
    TError SetLogException(Exception exception);
    
    TError AppendLogMessage([StructuredMessageTemplate] string message);
    
    TError AppendLogMessage([StructuredMessageTemplate] string message, params object[] args);
    
    TError PrependLogMessage([StructuredMessageTemplate] string message);
    
    TError PrependLogMessage([StructuredMessageTemplate] string message, params object[] args);
    
    TError IncludeLogLevelInToString(bool value);

    /// <summary>
    /// Overrides the current log builder with the one inside the specified error.
    /// This means that any log that this instance has will be overwritten by the logs of the given error.
    /// Use with caution.
    /// </summary>
    /// <typeparam name="TErrorType">The developer defined type of the error.</typeparam>
    public void OverrideLogBuilderWith<TErrorType>(LoggableHarmonyErrorCore<TErrorType> harmonyError)
        where TErrorType : class, ILoggableHarmonyError<TErrorType>;
    
    void Log();
}