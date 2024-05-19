using System.Diagnostics;
using Harmony.MinimalApis.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Todo.Domain.Errors;

public static partial class Errors
{
    // This is the most optimized way to perform logging in .NET in terms of performance. More information can be found
    // at: https://andrewlock.net/exploring-dotnet-6-part-8-improving-logging-performance-with-source-generators/
    // Specifically look for the section "The .NET 6 [LoggerMessage] source generator"
    
    [LoggerMessage(Level = LogLevel.Error,
        Message = "A request to access a resource was denied. Context error: '{ContextError}'. Trace Id: '{TraceId}'")]
    private static partial void LogAccessDenied(this ILogger logger, string? contextError, string traceId);
    
    [LoggerMessage(Level = LogLevel.Error,
        Message = "An attempt to get a new access token failed with refresh token: {RefreshToken}")]
    private static partial void LogInvalidRefreshToken(this ILogger logger, string refreshToken, Exception? ex);
    
    public static class Auth
    {
        public static HttpError AccessDenied(ILogger logger, string? contextError) => new (
            nameof(AccessDenied), 
            "You don't have access to the specified resource",
            StatusCodes.Status401Unauthorized,
            () => logger.LogAccessDenied(contextError, Activity.Current!.Id!));
        
        public static HttpError InvalidRefreshToken(ILogger logger, string refreshToken, Exception? ex = null) => new (
            nameof(InvalidRefreshToken), 
            "The refresh token is invalid",
            StatusCodes.Status401Unauthorized,
            () => logger.LogInvalidRefreshToken(refreshToken, ex));
    }
}