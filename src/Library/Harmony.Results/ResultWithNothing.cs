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
    
    public static Success<TMetadata> Ok<TMetadata>(Success<TMetadata> success)
    {
        return success;
    }
    
    public static Result<TValue, TError> Ok<TValue, TError>(TValue value, Success success)
    {
        return Result<TValue, TError>.Ok(value, success);
    }
    
    // TODO: Add support for TMetadata
    /*public static Result<TValue, TError> Ok<TValue, TError>(TValue value, Success<TMetadata> success)
    {
        return Result<TValue, TError>.Ok(value, success);
    }*/
    
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