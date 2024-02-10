using Harmony.MinimalApis.Mappers;
using Harmony.Results.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Harmony.MinimalApis.Errors;

public class HttpErrorResult : IResult
{
    private readonly IError _error;
    private readonly CancellationToken _cancellationToken;

    public HttpErrorResult(IError error, CancellationToken cancellationToken)
    {
        _error = error;
        _cancellationToken = cancellationToken;
    }

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        var statusCode = _error.HttpStatusCode;
        
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.Headers[HeaderNames.ContentType] = "application/json";
        
        await httpContext.Response.WriteAsJsonAsync(_error.MapToHttpError(),
            _cancellationToken);
    }
}