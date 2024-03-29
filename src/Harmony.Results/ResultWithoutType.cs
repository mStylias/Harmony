using Harmony.Results.Abstractions;

namespace Harmony.Results;

/// <summary>
/// The main result class for error handling without the need for exceptions
/// </summary>
public readonly record struct Result<TError> : IResultBase<TError>
{
    public TError? Error { get; }
    public Success? Success { get; }
    public bool IsError { get; }
    public bool IsSuccess => !IsError;
    
    internal Result(TError error)
    {
        Error = error;
        IsError = true;
        Success = null;
    }
    
    internal Result(Success? success)
    {
        Success = success;
        IsError = false;
        Error = default;
    }

    // Implicit operators
    public static implicit operator Result<TError>(TError error)
    {
        return Result<TError>.Fail(error);
    }
    
    public static implicit operator Result<TError>(Success success)
    {
        return new Result<TError>(success);
    }
    
    // Creator methods
    public static Result<TError> Fail(TError error)
    {
        return new Result<TError>(error);
    }

    public static Result<TValue, TError> Fail<TValue>(TError error)
    {
        return Result<TValue, TError>.Fail(error);
    }
    
    public static Result<TError> Ok()
    {
        Success? success = null;
        return new Result<TError>(success);
    }

    public static Result<TError> Ok(Success success)
    {
        return new Result<TError>(success);
    }
    
    public static Result<TValue, TError> Ok<TValue>(TValue value, Success success)
    {
        return Result<TValue, TError>.Ok(value, success);
    }
    
    public static Result<TValue, TError> Ok<TValue>(TValue value)
    {
        return Result<TValue, TError>.Ok(value);
    }
}