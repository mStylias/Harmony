using Harmony.MinimalApis.Errors;
using Harmony.MinimalApis.Mappers;
using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;

namespace Todo.Api.Endpoints.Todos.Get;

public class GetTodosOfUser : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}", (ILogger<GetTodosOfUser> logger) =>
            {
                var httpError = new HttpError("NoTodosExist",
                        "No todos exist for the authenticated user.", StatusCodes.Status404NotFound)
                    .InitializeLogMessage(logger, LogLevel.Error, "{User}", "User From Parameter")
                    .AppendLogMessage(" has no todos")
                    .PrependErrorCodeToLog()
                    .SetLogException(new InvalidOperationException("That is invalid."));

                httpError.Log();

                return httpError.MapToHttpResult();
            })
            .WithOpenApi(config =>
            {
                config.Summary = "Gets all todo lists along with their todos for the authenticated user";
                return config;
            });
    }
}