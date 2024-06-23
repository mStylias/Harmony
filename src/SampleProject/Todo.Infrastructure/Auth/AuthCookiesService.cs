using Microsoft.AspNetCore.Http;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Domain.Constants;

namespace Todo.Infrastructure.Auth;

public class AuthCookiesService : IAuthCookiesService
{
    /// <summary>
    /// Creates an http only secure cookie that contains the provided jwt access token.
    /// </summary>
    /// <param name="httpContext">The standard .NET Http Context</param>
    /// <param name="accessToken">The JWT access token to store in the cookie</param>
    /// <param name="expiration">The expiration DateTime of the cookie</param>
    public void SetAccessTokenCookie(HttpContext httpContext, string accessToken, DateTime expiration)
    {
        httpContext.Response.Cookies.Append(Constants.Auth.AccessTokenCookieName, accessToken,
            new CookieOptions
            {
                Expires = expiration,
                HttpOnly = true,
                SameSite = SameSiteMode.Unspecified,
                Secure = true,
                Path = "/"
            });
    }
}