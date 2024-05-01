using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Common.Constants;
using Todo.Application.Auth.Commands.Signup;
using Todo.Contracts.Auth;

namespace Todo.Api.Endpoints.Auth.Post;

public class Signup : IEndpoint
{
    public string Tag => EndpointTagNames.Auth;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost($"{EndpointBasePathNames.Auth}/signup", async Task<IResult> (
                [FromBody] SignupRequest signupRequest, 
                IOperationFactory operationFactory
            ) =>
        {
            using var signupCommand = operationFactory.SynthesizeOperation<SignupCommand, SignupRequest>(signupRequest);
            var result = await signupCommand.ExecuteAsync();
            if (result.IsError)
            {
                return result.Error.MapToHttpResult();
            }

            return Results.Ok(result.Value);
        })
        .AllowAnonymous();
    }
}