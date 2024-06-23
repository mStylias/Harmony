using System.Security.Claims;

namespace Todo.Api.Common.HttpContext;

public static class HttpContextExtensions
{
    public static string? GetUserId(this Microsoft.AspNetCore.Http.HttpContext httpContext)
    {
        var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
        var claimValue = claimsIdentity?.Claims.FirstOrDefault(c => 
            c.Type == Domain.Constants.Constants.Auth.UserIdClaimName);
    
        return claimValue?.Value;
    }
}