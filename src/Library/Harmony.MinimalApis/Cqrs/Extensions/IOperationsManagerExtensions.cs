using Harmony.Cqrs.Abstractions;
using Harmony.Cqrs.Operations;
using Harmony.MinimalApis.Errors;
using Harmony.MinimalApis.Mappers;
using Harmony.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Harmony.MinimalApis.Cqrs.Extensions;

public static class IOperationsManagerExtensions
{
    /// <summary>
    /// Executes the given operation and maps the result to an HTTP result.
    /// </summary>
    /// <typeparam name="TResponse">The response of the command.</typeparam>
    /// <returns>
    /// An <see cref="Microsoft.AspNetCore.Http.IResult"/> in case of success 
    /// or <see cref="Microsoft.AspNetCore.Http.HttpResults.ProblemHttpResult"/> in case of error.
    /// </returns>
    public static async Task<Results<IResult, ProblemHttpResult>> GetHttpResultOf<TResponse>(
        this IOperationsManager operationsManager,
        HarmonyOperation<Result<TResponse, HttpError>> operation,
        CancellationToken cancellationToken = default)
    {
        var result = await operationsManager
            .ExecuteOperationAsync(operation, cancellationToken)
            .ConfigureAwait(false);
        
        if (result.IsError)
        {
            result.Error.Log();
            return result.Error.ToProblemHttpResult();
        }
            
        return TypedResults.Ok(result.Value);
    }
    
    /// <summary>
    /// Executes the given operation and maps the result to an HTTP result.
    /// </summary>
    /// <returns>
    /// An <see cref="Microsoft.AspNetCore.Http.IResult"/> in case of success 
    /// or <see cref="Microsoft.AspNetCore.Http.HttpResults.ProblemHttpResult"/> in case of error.
    /// </returns>
    public static async Task<Results<IResult, ProblemHttpResult>> GetHttpResultOf(
        this IOperationsManager operationsManager,
        HarmonyOperation operation,
        CancellationToken cancellationToken = default)
    {
        await operationsManager
            .ExecuteOperationAsync(operation, cancellationToken)
            .ConfigureAwait(false);
            
        return TypedResults.Ok();
    }
}
