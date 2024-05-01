using Harmony.Results.ErrorTypes;
using Microsoft.AspNetCore.Http;

namespace Harmony.MinimalApis.Mappers;

public static class ValidationErrorMapper
{
    public static IResult MapToHttpResult(this ValidationError error, int statusCode)
    {
        var problem = Microsoft.AspNetCore.Http.Results.Problem(
            title: error.ErrorCode,
            detail: error.Description,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>
            {
                {"validationErrors", error.InnerErrors}
            });

        return problem;
    }
}