using Harmony.MinimalApis.Errors;
using Microsoft.AspNetCore.Http;

namespace Harmony.MinimalApis.Mappers;

public static class HttpErrorMapper
{
    /// <summary>
    /// Creates a <see cref="Microsoft.AspNetCore.Http.IResult"/> problem response from a harmony http error.
    /// </summary>
    /// <param name="error">The harmony http error</param>
    /// <returns>A Microsoft.AspNetCore.Http.IResult generated from the HttpError</returns>
    public static IResult MapToHttpResult(this HttpError error)
    {
        IResult problem;
        
        if (error.ValidationErrors.Count > 0)
        {
            problem = Microsoft.AspNetCore.Http.Results.Problem(
                title: error.ErrorCode,
                detail: error.Description,
                statusCode: error.HttpCode,
                extensions: new Dictionary<string, object?>
                {
                    {"validationErrors", error.ValidationErrors}
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
}