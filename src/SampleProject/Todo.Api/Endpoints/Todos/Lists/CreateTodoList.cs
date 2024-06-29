using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Common.Constants;
using Todo.Api.Common.HttpContext;
using Todo.Api.Common.Mappers;
using Todo.Application.Todos.Lists.Commands.CreateTodoList;
using Todo.Contracts.Todos.Lists.CreateTodoList;
using Todo.Domain.Errors;

namespace Todo.Api.Endpoints.Todos.Lists;

public class CreateTodoList : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost($"{EndpointBasePathNames.Todos}/lists", async Task<IResult> (
            HttpContext httpContext,
            [FromServices] ILogger<CreateTodoList> logger,
            [FromServices] IOperationFactory operationFactory,
            [FromBody] CreateTodoListRequest createTodoListRequest) =>
        {
            var userId = httpContext.GetUserId();
            if (userId is null)
            {
                return Errors.Auth.AccessDenied(logger, null).MapToHttpResult();
            }

            var commandInput = new CreateTodoListInput(
                userId,
                createTodoListRequest.Name,
                createTodoListRequest.Description);

            var createCommand = operationFactory.SynthesizeOperation<CreateTodoListCommand, CreateTodoListInput>(
                commandInput);
            
            var result = await createCommand.ExecuteAsync();
            if (result.IsError)
            {
                result.Error.Log();
                return result.Error.MapToHttpResult();
            }

            return Results.Ok(result.Value.MapToCreateTodoListResponse());
        }).WithOpenApi(config =>
        {
            config.Summary = "Creates a todo list for the authenticated user";
            return config;
        });
    }
}