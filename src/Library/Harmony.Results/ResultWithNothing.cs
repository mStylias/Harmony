namespace Harmony.Results;

public static class Result
{
    public static Success Ok()
    {
        return new Success();
    }
    
    public static Result<TError> Ok<TError>()
    {
        return Result<TError>.Ok();
    }
    
    public static Result<TValue, TError> Ok<TValue, TError>(TValue value)
    {
        return Result<TValue, TError>.Ok(value);
    }
    
    public static Result<TError> Fail<TError>(TError error)
    {
        return new Result<TError>(error);
    } 
    
    public static Result<TValue, TError> Fail<TValue, TError>(TError error)
    {
        return Result<TValue, TError>.Fail(error);
    } 
}