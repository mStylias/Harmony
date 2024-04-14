using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;

namespace Todo.Api.Endpoints.Auth.Post;

public class Signup : IEndpoint
{
    public string Tag => EndpointTagNames.Auth;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost($"{EndpointBasePathNames.Auth}/signup", () =>
        {
            throw new NotImplementedException();
        })
        .AllowAnonymous();
    }
}