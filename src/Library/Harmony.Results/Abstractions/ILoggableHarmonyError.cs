using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Harmony.Results.Abstractions;

public interface ILoggableHarmonyError : IHarmonyError
{
    ILoggableHarmonyError InitializeLogMessage(ILogger logger, LogLevel logLevel);
    ILoggableHarmonyError InitializeLogMessage(ILogger logger, LogLevel logLevel, EventId eventId);
    ILoggableHarmonyError SetLogException(Exception exception);
    ILoggableHarmonyError AppendLogMessage([StructuredMessageTemplate] string message);
    ILoggableHarmonyError AppendLogMessage([StructuredMessageTemplate] string message, params object[] args);
    ILoggableHarmonyError PrependLogMessage([StructuredMessageTemplate] string message);
    ILoggableHarmonyError PrependLogMessage([StructuredMessageTemplate] string message, params object[] args);
    void Log();
}