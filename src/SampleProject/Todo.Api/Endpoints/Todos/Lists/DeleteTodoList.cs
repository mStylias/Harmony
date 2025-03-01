using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Endpoints;
using Harmony.MinimalApis.Mappers;
using JetBrains.Annotations;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Application.Todos.Lists.Commands.DeleteTodoList;
using Todo.Domain.Errors;

namespace Todo.Api.Endpoints.Todos.Lists;

[UsedImplicitly]
internal class DeleteTodoList : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapDelete($"{EndpointBasePathNames.Todos}/lists/{{todoListId:int}}", async Task<IResult> (
                int todoListId,
                ILogger<DeleteTodoList> logger,
                HttpContext httpContext,
                IOperationsManager operationsManager) =>
            {
                var userId = httpContext.GetUserId();
                if (userId is null)
                {
                    return DomainErrors.Auth.AccessDenied(logger, null).MapToHttpResult();
                }

                var deleteOperation = operationsManager.CreateOperation<DeleteTodoListCommand>(c =>
                {
                    c.Input = new DeleteTodoListInput(todoListId, userId);
                });
                
                var deleteResult = await deleteOperation.ExecuteAsync();
                if (deleteResult.IsError)
                {
                    return deleteResult.Error.MapToHttpResult();
                }
                
                return Results.Ok();
            })
            .WithOpenApi(config =>
            {
                config.Summary = "Deletes the todo list with the given id and all it's todo items if it belongs to the logged in user";
                return config;
            });
    }
}