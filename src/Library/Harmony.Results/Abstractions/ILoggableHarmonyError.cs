using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Harmony.Results.Abstractions;

public interface ILoggableHarmonyError<out TError> : IHarmonyError
    where TError : class, ILoggableHarmonyError<TError>
{
    TError InitializeLogMessage(ILogger logger, LogLevel logLevel);
    TError InitializeLogMessage(ILogger logger, LogLevel logLevel, EventId eventId);
    TError SetLogException(Exception exception);
    TError AppendLogMessage([StructuredMessageTemplate] string message);
    TError AppendLogMessage([StructuredMessageTemplate] string message, params object[] args);
    TError PrependLogMessage([StructuredMessageTemplate] string message);
    TError PrependLogMessage([StructuredMessageTemplate] string message, params object[] args);
    void Log();
}