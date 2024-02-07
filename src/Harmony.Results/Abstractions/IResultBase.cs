namespace Harmony.Results.Abstractions;

public interface IResultBase
{
    IError? Error { get; }
    ISuccess? Success { get; }
    bool IsError { get; }
    bool IsSuccess { get; }
}