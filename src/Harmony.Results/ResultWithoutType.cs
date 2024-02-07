using Harmony.Results.Abstractions;

namespace Harmony.Results
{
    /// <summary>
    /// The main result class for error handling without the need for exceptions
    /// </summary>
    public readonly record struct Result : IResultBase
    {
        public IError? Error { get; }
        public ISuccess? Success { get; }
        public bool IsError { get; }
        public bool IsSuccess => !IsError;
        
        private Result(IError error)
        {
            Error = error;
            IsError = true;
        }
        
        private Result(ISuccess? success)
        {
            Success = success;
            IsError = false;
        }

        // Implicit operators
        public static implicit operator Result(Error error)
        {
            return Result.Fail(error);
        }
        
        public static implicit operator Result(Success success)
        {
            return new Result(success);
        }
        
        // Creator methods
        public static Result Fail(IError error)
        {
            return new Result(error);
        }
        
        public static Result Ok()
        {
            ISuccess? success = null;
            return new Result(success);
        }

        public static Result<TValue> Ok<TValue>(TValue value, ISuccess success)
        {
            return Result<TValue>.Ok(value, success);
        }
        
        public static Result<TValue> Ok<TValue>(TValue value)
        {
            return Result<TValue>.Ok(value);
        }
        
        public static Result Ok(ISuccess success)
        {
            return new Result(success);
        }
    }
}