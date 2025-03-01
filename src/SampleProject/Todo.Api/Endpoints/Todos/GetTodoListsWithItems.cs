using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Endpoints;
using Harmony.MinimalApis.Mappers;
using JetBrains.Annotations;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Application.Todos.Queries;

namespace Todo.Api.Endpoints.Todos;

[UsedImplicitly]
internal class GetTodoListsWithItems : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}", async Task<IResult> (
                HttpContext httpContext,
                IOperationsManager operationsManager) =>
            {
                var userId = httpContext.GetUserId();

                var query = operationsManager.CreateOperation<TodoListsWithItemsQuery>(q =>
                {
                    q.Input = userId;
                });

                var result = await operationsManager.ExecuteOperationAsync(query);
                if (result.IsError)
                {
                    // Here we don't want to log the error, but in another place we might have wanted to.
                    // Harmony gives us the flexibility to decide.
                    return result.Error.MapToHttpResult();
                }

                return Results.Ok(result.Value);
            })
            .WithOpenApi(config =>
            {
                config.Summary = "Gets all todo lists along with their todos for the logged on user";
                return config;
            });
    }
}