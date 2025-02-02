using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Common.Constants;
using Todo.Api.Common.Mappers;
using Todo.Application.Auth.Commands.Signup;
using Todo.Application.Common.Abstractions.Auth;
using Todo.Contracts.Auth;

namespace Todo.Api.Endpoints.Auth.Post;

public class Signup : IEndpoint
{
    public string Tag => EndpointTagNames.Auth;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost($"{EndpointBasePathNames.Auth}/signup", async Task<IResult> (
                HttpContext httpContext,
                [FromBody] SignupRequest signupRequest, 
                [FromServices] IOperationFactory operationFactory,
                [FromServices] IAuthCookiesService authCookiesService
            ) =>
        {
            var signupCommand = operationFactory.CreateBuilder<SignupCommand>()
                .WithInput(signupRequest)
                .Build();
            
            var signupResult = await signupCommand.ExecuteAsync();
            if (signupResult.IsError)
            {
                signupResult.Error.Log();
                return signupResult.Error.MapToHttpResult();
            }

            signupResult.Success?.Log();
            var authTokens = signupResult.Value;
            
            authCookiesService.SetAccessTokenCookie(httpContext, authTokens.AccessToken, 
                authTokens.AccessTokenExpiration);

            return Results.Ok(authTokens.MapToAuthResponse());
        })
        .AllowAnonymous();
    }
}