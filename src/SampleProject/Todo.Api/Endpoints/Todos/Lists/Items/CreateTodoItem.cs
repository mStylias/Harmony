using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Api.Common.Mappers;
using Todo.Application.Todos.Lists.Items.Commands.CreateTodoItem;
using Todo.Contracts.Todos.Lists.Items.CreateTodoItem;
using Todo.Domain.Enums.Todos;
using Todo.Domain.Errors;

namespace Todo.Api.Endpoints.Todos.Lists.Items;

public class CreateTodoItem : IEndpoint
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
            [FromServices] IOperationFactory operationFactory,
            [FromBody] CreateTodoItemRequest createTodoItemRequest) =>
        {
            var userId = httpContext.GetUserId();
            if (userId is null)
            {
                return Errors.Auth.AccessDenied(logger, null).MapToHttpResult();
            }

            var (name, description, todoStatus) = createTodoItemRequest;

            var createCommand = operationFactory.GetBuilder<CreateTodoItemCommand>()
                .WithInput(new CreateTodoItemInput(name, description, todoStatus, listId, userId))
                .Build();
            
            var result = await createCommand.ExecuteAsync();
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