using Harmony.Results.Abstractions;

namespace Harmony.Results;

/// <summary>
/// The main result class for error handling without the need for exceptions
/// </summary>
/// <typeparam name="TValue">The value type that is returned on success</typeparam>
/// /// <typeparam name="TError">The error type that is returned on failure</typeparam>
public readonly record struct Result<TValue, TError> : IResult<TValue, TError>
{
    public TValue? Value { get; }
    public TError? Error { get; }
    public Success? Success { get; }
    public bool IsError { get; }
    public bool IsSuccess => !IsError;

    private Result(TError error)
    {
        Error = error;
        IsError = true;
        Value = default;
        Success = null;
    }
    
    private Result(TValue? value)
    {
        Value = value;
        IsError = false;
        Error = default;
        Success = null;
    }

    private Result(TValue? value, Success? success)
    {
        Value = value;
        Success = success;
        IsError = false;
        Error = default;
    }

    // Implicit operators
    public static implicit operator Result<TValue, TError>(TValue value)
    {
        return new Result<TValue, TError>(value);
    }
    
    public static implicit operator Result<TValue, TError>(TError error)
    {
        return new Result<TValue, TError>(error);
    }

    public static implicit operator Result<TValue, TError>(Result<TError> resultWithoutType)
    {
        return resultWithoutType.IsError 
            ? new Result<TValue, TError>(resultWithoutType.Error!) 
            : new Result<TValue, TError>(default, resultWithoutType.Success);
    }
    
    // Creator methods
    public static Result<TValue, TError> Fail(TError error)
    {
        return new Result<TValue, TError>(error);
    }
    
    public static Result<TValue, TError> Ok()
    {
        return new Result<TValue, TError>(default(TValue));
    }
    
    public static Result<TValue, TError> Ok(Success success)
    {
        return new Result<TValue, TError>(default(TValue), success);
    }
    
    public static Result<TValue, TError> Ok(TValue value)
    {
        return new Result<TValue, TError>(value);
    }
    
    public static Result<TValue, TError> Ok(TValue value, Success success)
    {
        return new Result<TValue, TError>(value, success);
    }
}