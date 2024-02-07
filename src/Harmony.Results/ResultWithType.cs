using Harmony.Results.Abstractions;

namespace Harmony.Results
{
    /// <summary>
    /// The main result class for error handling without the need for exceptions
    /// </summary>
    /// <typeparam name="TValue">The value type that is returned on success</typeparam>
    public readonly record struct Result<TValue> : IResult<TValue>
    {
        public TValue? Value { get; }
        public IError? Error { get; }
        public ISuccess? Success { get; }
        public bool IsError { get; }
        public bool IsSuccess => !IsError;

        private Result(IError error)
        {
            Error = error;
            IsError = true;
        }
        
        private Result(TValue? value)
        {
            Value = value;
            IsError = false;
        }

        private Result(TValue? value, ISuccess? success)
        {
            Value = value;
            Success = success;
            IsError = false;
        }

        // Implicit operators
        public static implicit operator Result<TValue>(TValue value)
        {
            return new Result<TValue>(value);
        }
        
        public static implicit operator Result<TValue>(Error error)
        {
            return new Result<TValue>(error);
        }

        public static implicit operator Result<TValue>(Result resultWithoutType)
        {
            return resultWithoutType.IsError 
                ? new Result<TValue>(resultWithoutType.Error!) 
                : new Result<TValue>(default(TValue), resultWithoutType.Success);
        }
        
        // Creator methods
        public static Result<TValue> Fail(IError error)
        {
            return new Result<TValue>(error);
        }
        
        public static Result<TValue> Ok()
        {
            return new Result<TValue>(default(TValue));
        }
        
        public static Result<TValue> Ok(ISuccess success)
        {
            return new Result<TValue>(default(TValue), success);
        }
        
        public static Result<TValue> Ok(TValue value)
        {
            return new Result<TValue>(value);
        }
        
        public static Result<TValue> Ok(TValue value, ISuccess success)
        {
            return new Result<TValue>(value, success);
        }
    }
}