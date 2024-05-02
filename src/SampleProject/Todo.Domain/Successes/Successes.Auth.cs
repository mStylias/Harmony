using Harmony.Results;
using Microsoft.Extensions.Logging;

namespace Todo.Domain.Successes;

public static partial class Successes
{
    [LoggerMessage(Level = LogLevel.Information,
        Message = "User with email '{Email}' signed up successfully")]
    private static partial void LogSignupSuccess(this ILogger logger, string email);
    
    public static class Auth
    {
        public static Success SignupSuccess(ILogger logger, string email) => new(
            () => logger.LogSignupSuccess(email));
    } 
}