using Harmony.Results.Abstractions;

namespace Harmony.Results;

public static class Result
{
    public static Success Ok()
    {
        return new Success();
    }
    
    public static Success Ok(Success success)
    {
        return success;
    }
    
    public static Result<TValue, TError> Ok<TValue, TError>(TValue value, Success success) where TError : IHarmonyError
    {
        return Result<TValue, TError>.Ok(value, success);
    }
    
    public static Result<TError> Ok<TError>() where TError : IHarmonyError
    {
        return Result<TError>.Ok();
    }
    
    public static Result<TValue, TError> Ok<TValue, TError>(TValue value) where TError : IHarmonyError
    {
        return Result<TValue, TError>.Ok(value);
    }
    
    public static Result<TError> Fail<TError>(TError error) where TError : IHarmonyError
    {
        return new Result<TError>(error);
    } 
    
    public static Result<TValue, TError> Fail<TValue, TError>(TError error) where TError : IHarmonyError
    {
        return Result<TValue, TError>.Fail(error);
    } 
}