namespace Harmony.Results.Abstractions;

public interface IResultBase<out TError>
{
    TError? Error { get; }
    Success? Success { get; }
    bool IsError { get; }
    bool IsSuccess { get; }
    void LogSuccess();
}