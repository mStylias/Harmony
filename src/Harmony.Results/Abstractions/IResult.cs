namespace Harmony.Results.Abstractions;

public interface IResult<out TValue> : IResultBase
{
    TValue? Value { get; }
}