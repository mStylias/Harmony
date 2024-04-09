using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;

namespace Todo.Api.Endpoints.Todos.Get;

public class GetTodosOfList : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}/lists/{{todoListId}}", (int todoListId) =>
        {
            throw new NotImplementedException();
        })
        .WithOpenApi(config =>
        {
            config.Summary = "Gets all todos in the list with the given id.";
            return config;
        });
    }
}