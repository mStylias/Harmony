using Harmony.Results;
using Microsoft.Extensions.Logging;

namespace Todo.Domain.Successes;

public static partial class Successes
{
    [LoggerMessage(Level = LogLevel.Information,
        Message = "User with email '{Email}' logged in successfully")]
    private static partial void LogLoginSuccess(this ILogger logger, string email);
    
    [LoggerMessage(Level = LogLevel.Information,
        Message = "User with email '{Email}' signed up successfully")]
    private static partial void LogSignupSuccess(this ILogger logger, string email);
    
    [LoggerMessage(Level = LogLevel.Information,
        Message = "User with id '{UserId}' successfully refreshed their access token")]
    private static partial void LogRefreshTokenSuccess(this ILogger logger, string userId);
    
    public static class Auth
    {
        public static Success LoginSuccess(ILogger logger, string email) => new(
            () => logger.LogLoginSuccess(email));
        public static Success SignupSuccess(ILogger logger, string email) => new(
            () => logger.LogSignupSuccess(email));
        public static Success RefreshTokenSuccess(ILogger logger, string userId) => new(
            () => logger.LogRefreshTokenSuccess(userId));
    } 
}