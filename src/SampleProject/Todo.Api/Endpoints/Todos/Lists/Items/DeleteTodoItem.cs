using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Application.Todos.Lists.Items.Commands.DeleteTodoItem;
using Todo.Domain.Errors;

namespace Todo.Api.Endpoints.Todos.Lists.Items;

public class DeleteTodoItem : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapDelete($"{EndpointBasePathNames.Todos}/lists/{{todoListId:int}}/items/{{todoItemId:int}}", 
    async Task<IResult> (
                int todoListId,
                int todoItemId,
                ILogger<DeleteTodoList> logger,
                HttpContext httpContext,
                IOperationFactory operationFactory) =>
            {
                var userId = httpContext.GetUserId();
                if (userId is null)
                {
                    return Errors.Auth.AccessDenied(logger, null).MapToHttpResult();
                }

                var deleteOperation = operationFactory.GetBuilder<DeleteTodoItemCommand>()
                    .WithInput(new DeleteTodoItemInput(todoItemId, todoListId, userId))
                    .Build();
                
                var deleteResult = await deleteOperation.ExecuteAsync();
                if (deleteResult.IsError)
                {
                    return deleteResult.Error.MapToHttpResult();
                }
                
                return Results.Ok();
            })
            .WithOpenApi(config =>
            {
                config.Summary = "Deletes the todo item with the given item id and list id " +
                                 "if it belongs to the logged in user";
                return config;
            });;
    }
}