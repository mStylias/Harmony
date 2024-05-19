using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Common.Constants;
using Todo.Api.Common.Mappers;
using Todo.Application.Auth.Commands.RefreshToken;
using Todo.Application.Common.Abstractions.Auth;

namespace Todo.Api.Endpoints.Auth.Post;

public class Refresh : IEndpoint
{
    public string Tag => EndpointTagNames.Auth;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost($"{EndpointBasePathNames.Auth}/refresh", async Task<IResult> (
                HttpContext httpContext,
                [FromBody] RefreshRequest refreshRequest,
                [FromServices] IAuthCookiesService authCookiesService,
                [FromServices] IOperationFactory operationFactory
            ) =>
            {
                var refreshTokenCommand = operationFactory
                    .SynthesizeOperation<RefreshTokenCommand, RefreshRequest>(refreshRequest);

                var refreshResult = await refreshTokenCommand.ExecuteAsync();
                if (refreshResult.IsError)
                {
                    refreshResult.Error.Log();
                    return refreshResult.Error.MapToHttpResult();
                }

                var authTokensModel = refreshResult.Value!;
                
                authCookiesService.SetAccessTokenCookie(httpContext, authTokensModel.AccessToken, 
                    authTokensModel.AccessTokenExpiration);
                
                return Results.Ok(authTokensModel.MapToAuthResponse());
            })
            .AllowAnonymous();
    }
}