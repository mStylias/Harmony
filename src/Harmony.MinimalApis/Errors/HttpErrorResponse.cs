using Harmony.Results;

namespace Harmony.MinimalApis.Errors;

public record HttpErrorResponse
(
    string? TraceId,
    int HttpStatusCode,
    List<InnerError>? Errors
);