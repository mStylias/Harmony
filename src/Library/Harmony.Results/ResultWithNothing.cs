using System.Diagnostics.CodeAnalysis;
using Harmony.Results.Abstractions;

namespace Harmony.Results;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Reviewed.")]
public static class Result
{
    private static readonly Success SuccessForOk = new(); 
    
    public static Success Ok()
    {
        return SuccessForOk;
    }
    
    public static Success Ok(Success success)
    {
        return success;
    }
    
    public static Result<TValue, TError> Ok<TValue, TError>(TValue value, Success success) 
        where TError : IHarmonyError
    {
        return Result<TValue, TError>.Ok(value, success);
    }
    
    public static Result<TError> Ok<TError>() 
        where TError : IHarmonyError
    {
        return Result<TError>.Ok();
    }
    
    public static Result<TValue, TError> Ok<TValue, TError>(TValue value) 
        where TError : IHarmonyError
    {
        return Result<TValue, TError>.Ok(value);
    }
    
    public static Result<TError> Fail<TError>(TError error) 
        where TError : IHarmonyError
    {
        return new Result<TError>(error);
    } 
    
    public static Result<TValue, TError> Fail<TValue, TError>(TError error) 
        where TError : IHarmonyError
    {
        return Result<TValue, TError>.Fail(error);
    } 
}