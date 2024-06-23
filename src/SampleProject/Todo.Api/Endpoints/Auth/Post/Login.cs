using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;

using Microsoft.AspNetCore.Mvc;
using Todo.Api.Common.Constants;
using Todo.Api.Common.Mappers;
using Todo.Application.Auth.Queries.Login;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Contracts.Auth;

namespace Todo.Api.Endpoints.Auth.Post;

public class Login : IEndpoint
{
    public string Tag => EndpointTagNames.Auth;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost($"{EndpointBasePathNames.Auth}/login", async Task<IResult>(
            HttpContext httpContext,
            [FromBody] LoginRequest loginRequest,
            [FromServices] IOperationFactory operationFactory,
            [FromServices] IAuthCookiesService authCookiesService) =>
        {
            var loginQuery = operationFactory.SynthesizeOperation<LoginQuery, LoginRequest>(loginRequest);
            
            var loginResult = await loginQuery.ExecuteAsync();
            if (loginResult.IsError)
            {
                loginResult.Error.Log();
                return loginResult.Error.MapToHttpResult();
            }

            var tokensModel = loginResult.Value;
            authCookiesService.SetAccessTokenCookie(httpContext, tokensModel.AccessToken, 
                tokensModel.AccessTokenExpiration);
            
            return Results.Ok(tokensModel.MapToResponse());
        })
        .AllowAnonymous();
    }
}
