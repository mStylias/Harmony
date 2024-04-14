#if (NET8_0_OR_GREATER)

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Harmony.MinimalApis.Exceptions;

public class HarmonyGlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<HarmonyGlobalExceptionHandler> _logger;

    public HarmonyGlobalExceptionHandler(ILogger<HarmonyGlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        IResult problem;

        switch (exception)
        {
            case OperationCanceledException canceledException:
                _logger.LogWarning(canceledException, "Operation cancelled");

                problem = Microsoft.AspNetCore.Http.Results.Problem(
                    title: "OperationCancelled",
                    detail: "An operation was cancelled",
                    statusCode: StatusCodes.Status499ClientClosedRequest);

                httpContext.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
                break;

            default:
                _logger.LogError(exception, "An unhandled exception occurred");

                problem = Microsoft.AspNetCore.Http.Results.Problem(
                    title: "UnhandledException",
                    detail: "An unhandled exception occurred",
                    statusCode: StatusCodes.Status500InternalServerError);

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        await problem.ExecuteAsync(httpContext);
        
        return true;
    }
}

#endif