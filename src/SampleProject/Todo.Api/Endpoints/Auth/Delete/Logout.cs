using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;

namespace Todo.Api.Endpoints.Auth.Delete;

public class Logout : IEndpoint
{
    public string Tag => EndpointTagNames.Auth;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapDelete($"{EndpointBasePathNames.Auth}/logout", () =>
        {
            throw new NotImplementedException();
        });
    }
}