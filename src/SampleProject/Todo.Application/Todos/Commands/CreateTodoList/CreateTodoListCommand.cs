using Harmony.Cqrs;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.Extensions.Logging;

namespace Todo.Application.Todos.Commands.CreateTodoList;

public class CreateTodoListCommand : Command<Result<HttpError>>
{
    private readonly ILogger<CreateTodoListCommand> _logger;

    public CreateTodoListCommand(ILogger<CreateTodoListCommand> logger)
    {
        _logger = logger;
    }
    
    public override Task<Result<HttpError>> ExecuteAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}