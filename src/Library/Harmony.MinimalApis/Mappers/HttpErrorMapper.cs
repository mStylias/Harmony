using Harmony.MinimalApis.Errors;
using Microsoft.AspNetCore.Http;

namespace Harmony.MinimalApis.Mappers;

public static class HttpErrorMapper
{
    public static IResult MapToHttpResult(this HttpError error)
    {
        var problem = Microsoft.AspNetCore.Http.Results.Problem(
            title: error.ErrorCode,
            detail: error.Description,
            statusCode: error.HttpCode);

        return problem;
    }
}