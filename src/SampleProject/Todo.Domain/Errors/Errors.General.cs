using System.Text.Json;
using System.Xml;
using Harmony.MinimalApis.Errors;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Logging;

namespace Todo.Domain.Errors;

public static partial class Errors
{
    [LoggerMessage(Message = "Entity '{EntityName}' was null", Level = LogLevel.Error)]
    private static partial void LogNullReference(this ILogger logger, string entityName);

    [LoggerMessage(Message = "Validation failed with errors: {ValidationErrors}", Level = LogLevel.Error)]
    private static partial void LogValidationFailed(this ILogger logger, string validationErrors);
    
    public static class General
    {
        public static HttpError NullReferenceError(ILogger logger, string entityName) => new(
            nameof(NullReferenceError), 
            "Null reference error",
            StatusCodes.Status400BadRequest,
            () => logger.LogNullReference(entityName));

        public static HttpError ValidationError(ILogger logger, List<ValidationInnerError> validationErrors) => new(
            nameof(ValidationError),
            "Input was not valid",
            StatusCodes.Status400BadRequest,
            validationErrors,
            () => logger.LogValidationFailed(JsonSerializer.Serialize(validationErrors, JsonOptions))
            );
    }
}