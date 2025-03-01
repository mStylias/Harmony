using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Endpoints;
using Harmony.MinimalApis.Mappers;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Common.Constants;
using Todo.Api.Common.Mappers;
using Todo.Application.Auth.Queries.Login;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Contracts.Auth;

namespace Todo.Api.Endpoints.Auth.Post;

[UsedImplicitly]
internal class Login : IEndpoint
{
    public string Tag => EndpointTagNames.Auth;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost($"{EndpointBasePathNames.Auth}/login", async Task<IResult> (
            HttpContext httpContext,
            CancellationToken cancellationToken,
            [FromBody] LoginRequest loginRequest,
            [FromServices] IOperationsManager operationsManager,
            [FromServices] IAuthCookiesService authCookiesService) =>
            {
                var loginQuery = operationsManager.CreateOperation<LoginQuery>(query =>
                {
                    query.LoginRequest = loginRequest;
                });
                
                var loginResult = await operationsManager.ExecuteOperationAsync(loginQuery, cancellationToken);
                    
                if (loginResult.IsError)
                {
                    loginResult.Error.Log();
                    return loginResult.Error.MapToHttpResult();
                }

                var tokensModel = loginResult.Value;
                authCookiesService.SetAccessTokenCookie(
                    httpContext, 
                    tokensModel.AccessToken, 
                    tokensModel.AccessTokenExpiration);
                
                return Results.Ok(tokensModel.MapToAuthResponse());
            })
            .AllowAnonymous();
    }
}
