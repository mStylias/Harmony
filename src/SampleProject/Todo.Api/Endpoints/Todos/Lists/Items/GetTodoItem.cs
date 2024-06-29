using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Api.Common.Mappers;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Errors;
using Todo.Domain.Errors.Inner;

namespace Todo.Api.Endpoints.Todos.Lists.Items;

public class GetTodoItem : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}/lists/{{todoListId:int}}/items/{{todoItemId:int}}", 
            async Task<IResult> (
                int todoListId,
                int todoItemId,
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
                
                var todoItem = await todosRepository
                    .GetTodoItemById(todoItemId, cancellationToken);
                if (todoItem is null)
                {
                    return Errors.General.ValidationError(logger, [
                        new(
                            InnerErrorCodes.Validation.EntityDoesNotExist, 
                            "Item not found", 
                            nameof(todoItemId))
                    ]).MapToHttpResult();
                }
                
                return Results.Ok(todoItem.MapToGetTodoItemResponse());
            })
            .WithOpenApi(config =>
            {
                config.Summary = "Gets the items of the given list by it's id if it belong to the logged on user";
                return config;
            });
    }
}