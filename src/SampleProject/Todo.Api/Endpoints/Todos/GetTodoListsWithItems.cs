using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Application.Todos.Queries;

namespace Todo.Api.Endpoints.Todos;

public class GetTodoListsWithItems : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}", async Task<IResult> (
            HttpContext httpContext,
            IOperationFactory operationFactory) =>
        {
            var userId = httpContext.GetUserId();

            var query = operationFactory.SynthesizeOperation<TodoListsWithItemsQuery, string?>(userId);

            var result = await query.ExecuteAsync();
            if (result.IsError)
            {
                // Here we don't want to log the error, but in another place we might have wanted to.
                // Harmony gives us the flexibility to decide.
                return result.Error.MapToHttpResult();
            }

            return Results.Ok(result.Value);
        });
    }
}