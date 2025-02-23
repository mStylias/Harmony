namespace Harmony.Results.Abstractions;

public interface IResultBase<out TError>
{
#pragma warning disable CA1716
    TError? Error { get; }
#pragma warning restore CA1716
    
    Success? Success { get; }
    
    bool IsError { get; }
    
    bool IsSuccess { get; }
    
    bool IsWarning { get; }
    
    void LogSuccess();
}