using Microsoft.AspNetCore.Http;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Infrastructure.Common.Constants;

namespace Todo.Infrastructure.Auth;

public class AuthCookiesService : IAuthCookiesService
{
    public void SetAccessTokenCookie(HttpContext httpContext, string accessToken, DateTime expiration)
    {
        httpContext.Response.Cookies.Append(Constants.Auth.AccessTokenCookieName, accessToken,
            new CookieOptions
            {
                Expires = expiration,
                HttpOnly = true,
                SameSite = SameSiteMode.Unspecified,
                Secure = false,
                Path = "/"
            });
    }
}