using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Api.Common.Mappers;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Errors;
using Todo.Domain.Errors.Inner;

namespace Todo.Api.Endpoints.Todos.Lists;

public class GetTodoList : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}/lists/{{todoListId:int}}", async Task<IResult>(
                int todoListId,
                ILogger<GetTodoList> logger,
                HttpContext httpContext,
                ITodosRepository todosRepository,
                CancellationToken cancellationToken) =>
        {
            var userId = httpContext.GetUserId();
            
            var todoList = await todosRepository.GetTodoListById(todoListId, cancellationToken);
            if (todoList is null)
            {
                return Errors.General.ValidationError(logger, [
                    new ValidationInnerError(
                        InnerErrorCodes.Validation.EntityDoesNotExist,
                        "No todo list found for the given ID",
                        nameof(todoListId))
                ]).MapToHttpResult();
            }
            
            if (todoList.UserId != userId)
            {
                return Errors.Auth.AccessDenied(logger, null).MapToHttpResult();
            }
            
            return Results.Ok(todoList.MapToGetTodoListResponse());
        })
        .WithOpenApi(config =>
        {
            config.Summary = "Gets a specific list by it's id if it belongs to the logged on user";
            return config;
        });
    }
}