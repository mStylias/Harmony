using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;

namespace Todo.Api.Endpoints.Todos.Get;

public class GetTodoListsOfUser : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}/lists", () =>
            {
                return Results.Ok();
            })
            .WithOpenApi(config =>
            {
                config.Summary = "Gets all todo lists without their inner todos for the authenticated user.";
                return config;
            });
    }
}