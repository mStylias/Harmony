using Harmony.MinimalApis.Structure;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Common.Constants;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Contracts.Auth;

namespace Todo.Api.Endpoints.Auth.Delete;

public class Logout : IEndpoint
{
    public string Tag => EndpointTagNames.Auth;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapDelete($"{EndpointBasePathNames.Auth}/logout", async Task<IResult> (
            [FromBody] LogoutRequest logoutRequest,
            [FromServices] IAuthRepository authRepository) =>
        {
            await authRepository.DeleteRefreshToken(logoutRequest.RefreshToken);
            return Results.Ok();
        });
    }
}
