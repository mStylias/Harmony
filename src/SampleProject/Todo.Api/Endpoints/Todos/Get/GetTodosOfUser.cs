﻿using Harmony.MinimalApis.Structure;
using Todo.Api.Common.Constants;

namespace Todo.Api.Endpoints.Todos.Get;

public class GetTodosOfUser : IEndpoint
{
    public string Tag => EndpointTagNames.Todos;
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{EndpointBasePathNames.Todos}", () =>
        {
            throw new NotImplementedException();
        })
        .WithOpenApi(config =>
        {
            config.Summary = "Gets all todo lists along with their todos for the authenticated user.";
            return config;
        });
    }
}