namespace Harmony.Results.Abstractions;

public interface ILoggableHarmonyError : IHarmonyError
{ 
    void Log();
}