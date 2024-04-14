using Microsoft.AspNetCore.Http;

namespace Todo.Application.Common.Abstractions.Auth;

public interface IAuthCookiesService
{
    void SetAccessTokenCookie(HttpContext httpContext, string accessToken, DateTime expiration);
}