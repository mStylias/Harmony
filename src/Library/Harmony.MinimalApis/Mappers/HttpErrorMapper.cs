using Harmony.MinimalApis.Errors;
using Microsoft.AspNetCore.Http;

namespace Harmony.MinimalApis.Mappers;

public static class HttpErrorMapper
{
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