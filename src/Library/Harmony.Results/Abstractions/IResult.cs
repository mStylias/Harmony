namespace Harmony.Results.Abstractions;

public interface IResult<out TValue, TError> : IResultBase<TError>
{
    TValue? Value { get; }
}