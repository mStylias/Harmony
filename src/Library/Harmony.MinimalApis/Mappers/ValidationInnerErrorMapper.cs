using Harmony.MinimalApis.Errors;
using Harmony.Results.ErrorTypes.InnerErrorTypes;

namespace Harmony.MinimalApis.Mappers;

public static class ValidationInnerErrorMapper
{
    public static HttpError MapToHttpError(this ValidationInnerError inputError, int httpCode = 400)
    {
        var httpError = new HttpError(
            inputError.Code,
            inputError.Description,
            httpCode,
            new List<ValidationInnerError> { inputError });
        
        httpError.OverrideLogBuilderWith(inputError);

        return httpError;
    }
}