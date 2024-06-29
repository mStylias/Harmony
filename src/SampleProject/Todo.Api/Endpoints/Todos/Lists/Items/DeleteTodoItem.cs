using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;

namespace Todo.Api.Endpoints.Todos.Lists.Items;

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
            config.Summary = "Deletes the item in the specified list by item id and list id if it belongs to the logged on user";
            return config;
        });
    }
}