using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Endpoints;
using Harmony.MinimalApis.Mappers;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Api.Common.Mappers;
using Todo.Application.Todos.Lists.Items.Commands.CreateTodoItem;
using Todo.Contracts.Todos.Lists.Items.CreateTodoItem;
using Todo.Domain.Enums.Todos;
using Todo.Domain.Errors;

namespace Todo.Api.Endpoints.Todos.Lists.Items;

[UsedImplicitly]
internal class CreateTodoItem : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        // This transforms the enum in an id - name format for the OpenApi documentation, so we don't have to manually 
        // keep the values in sync when modifying the enum.
        var todosStatuses = Enum
            .GetValues<TodoStatus>()
            .Select(x => $"{(int)x} - {x.ToString()}");
        
        return app.MapPost($"{EndpointBasePathNames.Todos}/lists/{{listId:int}}/items", async Task<IResult> (
            int listId,
            HttpContext httpContext,
            [FromServices] ILogger<CreateTodoList> logger,
            [FromServices] IOperationsManager operationFactory,
            [FromBody] CreateTodoItemRequest createTodoItemRequest) =>
        {
            var userId = httpContext.GetUserId();
            if (userId is null)
            {
                return DomainErrors.Auth.AccessDenied(logger, null).MapToHttpResult();
            }

            var createCommand = operationFactory.CreateOperation<CreateTodoItemCommand>(c =>
            {
                c.TodoName = createTodoItemRequest.Name;
                c.TodoDescription = createTodoItemRequest.Description;
                c.TodoStatus = createTodoItemRequest.Status;
                c.TodoListId = listId;
                c.UserId = userId;
            });
            
            var result = await operationFactory.ExecuteOperationAsync(createCommand);
            if (result.IsError)
            {
                result.Error.Log();
                return result.Error.MapToHttpResult();
            }

            return Results.Ok(result.Value.MapToCreateTodoItemResponse());
        }).WithOpenApi(config =>
        {
            config.Summary = "Gets a list item by the list id and the item id";
            config.Description = $"The available todo statuses are: <br>" +
                                 $"{todosStatuses.Aggregate((x, y) => $"{x}<br>{y}")}";
            return config;
        });
    }
}