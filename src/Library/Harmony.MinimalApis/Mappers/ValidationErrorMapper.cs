using Harmony.Results.ErrorTypes;
using Microsoft.AspNetCore.Http;

namespace Harmony.MinimalApis.Mappers;

public static class ValidationErrorMapper
{
    public static IResult MapToHttpResult(this ValidationError error)
    {
        var errors = new Dictionary<string, object?>();

        foreach (var innerError in error.InnerErrors)
        {
            errors.Add("ErrorCode", innerError.Code);
            errors.Add("PropertyName", innerError.PropertyName);
        }
        
        var problem = Microsoft.AspNetCore.Http.Results.Problem(
            title: error.ErrorCode,
            detail: error.Description,
            statusCode: StatusCodes.Status400BadRequest,
            extensions: errors);

        return problem;
    }
}