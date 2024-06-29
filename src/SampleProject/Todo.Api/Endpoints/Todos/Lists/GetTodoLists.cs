using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Api.Common.Mappers;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Errors;

namespace Todo.Api.Endpoints.Todos.Lists;

public class GetTodoLists : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}/lists", async Task<IResult>(
                ILogger<GetTodoList> logger,
                HttpContext httpContext,
                ITodosRepository todosRepository,
                CancellationToken cancellationToken) =>
            {
                var userId = httpContext.GetUserId();
                if (userId is null)
                {
                    return Errors.Auth.AccessDenied(logger, null).MapToHttpResult();
                }
            
                var todoList = await todosRepository
                    .GetTodoListsOfUserAsync(userId, cancellationToken);
            
                return Results.Ok(todoList);
            })
            .WithOpenApi(config =>
            {
                config.Summary = "Gets a specific list by it's id if it belongs to the logged on user";
                return config;
            });
    }
}