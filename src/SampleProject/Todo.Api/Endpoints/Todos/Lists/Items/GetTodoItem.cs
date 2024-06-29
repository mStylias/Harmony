using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;

namespace Todo.Api.Endpoints.Todos.Lists.Items;

public class GetTodoItem : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}/lists/{{todoListId:int}}/items/{{todoItemId:int}}", 
                async Task<IResult> (int todoListId) =>
            {
                return Results.Ok();
            })
            .WithOpenApi(config =>
            {
                config.Summary = "Gets a list item by the list id and the item id";
                return config;
            });
    }
}