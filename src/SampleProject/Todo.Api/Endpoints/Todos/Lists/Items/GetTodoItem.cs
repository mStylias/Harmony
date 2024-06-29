using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;

namespace Todo.Api.Endpoints.Todos.Lists.Items;

public class GetTodoItem : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}/lists/{{todoListId:int}}", (int todoListId) =>
            {
                return Results.Ok();
            })
            .WithOpenApi(config =>
            {
                config.Summary = "Gets the list with the given ID without its inner todos for the authenticated user";
                return config;
            });
    }
}