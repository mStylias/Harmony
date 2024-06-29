using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;

namespace Todo.Api.Endpoints.Todos.Lists;

public class DeleteTodoList : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapDelete($"{EndpointBasePathNames.Todos}/lists/{{todoListId:int}}", (int todoListId) =>
            {
                return Results.Ok();
            })
            .WithOpenApi(config =>
            {
                config.Summary = "Deletes the todo list with the given id and all it's todo items";
                return config;
            });;
    }
}