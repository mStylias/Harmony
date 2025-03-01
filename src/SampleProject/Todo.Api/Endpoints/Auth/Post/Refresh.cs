using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Endpoints;
using Harmony.MinimalApis.Mappers;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Common.Constants;
using Todo.Api.Common.Mappers;
using Todo.Application.Auth.Commands.RefreshToken;
using Todo.Application.Common.Abstractions.Auth;

namespace Todo.Api.Endpoints.Auth.Post;

[UsedImplicitly]
internal class Refresh : IEndpoint
{
    public string Tag => EndpointTagNames.Auth;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost($"{EndpointBasePathNames.Auth}/refresh", async Task<IResult> (
                HttpContext httpContext,
                [FromBody] RefreshRequest refreshRequest,
                [FromServices] IAuthCookiesService authCookiesService,
                [FromServices] IOperationsManager operationsManager) =>
            {
                var refreshTokenCommand = operationsManager.CreateOperation<RefreshTokenCommand>(c =>
                {
                    c.RefreshRequest = refreshRequest;
                });

                var refreshResult = await operationsManager.ExecuteOperationAsync(refreshTokenCommand);
                if (refreshResult.IsError)
                {
                    refreshResult.Error.Log();
                    return refreshResult.Error.MapToHttpResult();
                }

                var authTokensModel = refreshResult.Value;
                
                authCookiesService.SetAccessTokenCookie(
                    httpContext, 
                    authTokensModel.AccessToken, 
                    authTokensModel.AccessTokenExpiration);
                
                return Results.Ok(authTokensModel.MapToAuthResponse());
            })
            .AllowAnonymous();
    }
}