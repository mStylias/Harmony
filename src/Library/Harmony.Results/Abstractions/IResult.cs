namespace Harmony.Results.Abstractions;

public interface IResult<out TValue, out TError> : IResultBase<TError>
{
    TValue? Value { get; }
}