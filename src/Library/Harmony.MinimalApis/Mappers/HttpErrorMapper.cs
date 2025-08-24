using Harmony.MinimalApis.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Harmony.MinimalApis.Mappers;

public static class HttpErrorMapper
{
    /// <summary>
    /// Creates a <see cref="Microsoft.AspNetCore.Http.IResult"/> problem response from a harmony http error.
    /// </summary>
    /// <param name="error">The harmony http error.</param>
    /// <returns>A Microsoft.AspNetCore.Http.IResult generated from the HttpError.</returns>
    public static IResult MapToHttpResult(this HttpError error)
    {
        IResult problem;
        
        if (error.ValidationErrors?.Count > 0)
        {
            problem = Microsoft.AspNetCore.Http.Results.Problem(
                title: error.ErrorCode,
                detail: error.Description,
                statusCode: error.HttpCode,
                extensions: new Dictionary<string, object?>
                {
                    { "validationErrors", error.ValidationErrors },
                });
        }
        else
        {
            problem = Microsoft.AspNetCore.Http.Results.Problem(
                title: error.ErrorCode,
                detail: error.Description,
                statusCode: error.HttpCode);
        }

        return problem;
    }
    
    /// <summary>
    /// Creates a <see cref="Microsoft.AspNetCore.Http.HttpResults.ProblemHttpResult"/> problem response from a harmony http error.
    /// </summary>
    /// <param name="error">The harmony http error.</param>
    public static ProblemHttpResult ToProblemHttpResult(this HttpError error)
    {
        if (error.ValidationErrors?.Count > 0)
        {
            return TypedResults.Problem(
                title: error.ErrorCode,
                detail: error.Description,
                statusCode: error.HttpCode,
                extensions: new Dictionary<string, object?>
                {
                    ["validationErrors"] = error.ValidationErrors,
                });
        }

        return TypedResults.Problem(
            title: error.ErrorCode,
            detail: error.Description,
            statusCode: error.HttpCode);
    }
}