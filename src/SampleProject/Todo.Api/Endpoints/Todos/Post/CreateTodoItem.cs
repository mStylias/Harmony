using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Api.Common.Mappers;
using Todo.Application.Todos.Commands.CreateTodoItem;
using Todo.Contracts.Todos.CreateTodoItem;
using Todo.Domain.Enums.Todos;
using Todo.Domain.Errors;

namespace Todo.Api.Endpoints.Todos.Post;

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
        
        return app.MapPost($"{EndpointBasePathNames.Todos}/lists/items", async Task<IResult> (
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

            var createCommand = operationFactory.SynthesizeOperation<CreateTodoItemCommand, CreateTodoItemRequest>(
                createTodoItemRequest);
            
            var result = await createCommand.ExecuteAsync();
            if (result.IsError)
            {
                result.Error.Log();
                return result.Error.MapToHttpResult();
            }

            return Results.Ok(result.Value.MapToCreateTodoItemResponse());
        }).WithOpenApi(config =>
        {
            config.Summary = "Creates a todo list for the given list id";
            config.Description = $"The available todo statuses are: <br>" +
                                 $"{todosStatuses.Aggregate((x, y) => $"{x}<br>{y}")}";
            return config;
        });
    }
}