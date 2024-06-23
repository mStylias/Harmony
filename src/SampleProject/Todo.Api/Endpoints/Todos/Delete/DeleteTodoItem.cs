using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;

namespace Todo.Api.Endpoints.Todos.Delete;

public class DeleteTodoItem : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapDelete($"{EndpointBasePathNames.Todos}/{{todoItemId:int}}", (int todoItemId) =>
            {
                return Results.Ok();
            })
        .WithOpenApi(config =>
        {
            config.Summary = "Deletes the todo item with the given id";
            return config;
        });
    }
}