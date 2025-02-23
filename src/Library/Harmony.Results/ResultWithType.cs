using System.Diagnostics.CodeAnalysis;
using Harmony.Results.Abstractions;
using Harmony.Results.Enums;

namespace Harmony.Results;

/// <summary>
/// The main result class for error handling without the need for exceptions.
/// </summary>
/// <typeparam name="TValue">The value type that is returned on success.</typeparam>
/// <typeparam name="TError">The error type that is returned on failure.</typeparam>
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Reviewed.")]
public readonly record struct Result<TValue, TError> : IResult<TValue, TError> 
    where TError : IHarmonyError
{
    private Result(TError error)
    {
        this.Error = error;
        this.Value = default;
        this.Success = null;
    }
    
    private Result(TValue? value)
    {
        this.Value = value;
        this.Error = default;
        this.Success = null;
    }

    private Result(TValue? value, Success? success)
    {
        this.Value = value;
        this.Error = default;
        this.Success = success;
    }
    
    public TValue? Value { get; }
    
    public TError? Error { get; }
    
    public Success? Success { get; }
    
    [MemberNotNullWhen(true, nameof(Error))]
    [MemberNotNullWhen(false, nameof(Value))]
    public bool IsError => this.Error is not null && this.Error.Severity == Severity.Error;
    
    [MemberNotNullWhen(true, nameof(Error))]
    [MemberNotNullWhen(false, nameof(Value))]
    public bool IsWarning => this.Error is not null && this.Error.Severity == Severity.Warning;
    
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess => !this.IsError;

    // Implicit operators
#pragma warning disable CA2225
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
#pragma warning restore CA2225
    
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
    
    public void LogSuccess()
    {
        this.Success?.Log();
    }
}