using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Api.Common.Mappers;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Errors;

namespace Todo.Api.Endpoints.Todos.Get;

public class GetTodosListsOfUser : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}/lists", async (
                CancellationToken cancellationToken,
                ILogger<GetTodosListsOfUser> logger, 
                HttpContext httpContext, 
                ITodosRepository todosRepository) =>
            {
                var userId = httpContext.GetUserId();
                if (userId is null)
                {
                    return Errors.Auth.AccessDenied(logger, null).MapToHttpResult();
                }

                // Not to be confused with Harmony. This is the Microsoft Results class that is used to return
                // HTTP responses in minimal APIs.
                var todoLists = await todosRepository
                    .GetTodoListsOfUserAsync(userId, cancellationToken);
                return Results.Ok(todoLists.Select(x => x.MapToTodoListResponse()));
            })
            .WithOpenApi(config =>
            {
                config.Summary = "Gets all todo lists without their inner todos for the authenticated user";
                return config;
            });
    }
}