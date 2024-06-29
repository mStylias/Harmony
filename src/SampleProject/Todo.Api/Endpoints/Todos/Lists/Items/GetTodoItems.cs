using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Api.Common.Mappers;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Errors;

namespace Todo.Api.Endpoints.Todos.Lists.Items;

public class GetTodoItems : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}/lists/{{todoListId:int}}/items", 
                async Task<IResult> (
                    int todoListId,
                    ILogger<GetTodoItems> logger,
                    HttpContext httpContext,
                    ITodosRepository todosRepository,
                    CancellationToken cancellationToken) =>
                {
                    var userId = httpContext.GetUserId();
                    if (userId is null)
                    {
                        return Errors.Auth.AccessDenied(logger, null).MapToHttpResult();
                    }

                    bool userOwnsList = await todosRepository.UserOwnsListAsync(todoListId, userId, cancellationToken);
                    if (userOwnsList == false)
                    {
                        return Errors.Auth.AccessDenied(logger,null).MapToHttpResult();
                    }
                    
                    var todoItems = await todosRepository
                        .GetTodoListItemsAsync(todoListId, cancellationToken);
                    
                    return Results.Ok(todoItems.MapToGetTodoItemsResponse());
                })
            .WithOpenApi(config =>
            {
                config.Summary = "Gets all the items of the given list if it belong to the logged on user";
                return config;
            });
    }
}