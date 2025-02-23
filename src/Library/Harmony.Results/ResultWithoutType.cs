using System.Diagnostics.CodeAnalysis;
using Harmony.Results.Abstractions;
using Harmony.Results.Enums;

namespace Harmony.Results;

/// <summary>
/// The main result class for error handling without the need for exceptions.
/// </summary>
/// <typeparam name="TError">The developer defined error type.</typeparam>
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Reviewed.")]
public readonly record struct Result<TError> : IResultBase<TError> 
    where TError : IHarmonyError
{
    internal Result(TError error)
    {
        this.Error = error;
        this.Success = null;
    }

    private Result(Success? success)
    {
        this.Success = success;
        this.Error = default;
    }

    public TError? Error { get; }
    
    public Success? Success { get; }
    
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsError => this.Error is not null && this.Error.Severity == Severity.Error;
    
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsWarning => this.Error is not null && this.Error.Severity == Severity.Warning;
    
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess => !this.IsError;
    
    // Implicit operators
#pragma warning disable CA2225
    public static implicit operator Result<TError>(TError error)
    {
        return Fail(error);
    }
#pragma warning restore CA2225
    
    public static implicit operator Result<TError>(Success success)
    {
        return new Result<TError>(success);
    }
    
    // Creator methods
#pragma warning disable CA1000
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
#pragma warning restore CA1000
    
    public void LogSuccess()
    {
        this.Success?.Log();
    }

    public Result<TError> FromSuccess(Success success)
    {
        return new Result<TError>(success);
    }

    public Result<TError> FromError(TError error)
    {
        return Fail(error);
    }
}