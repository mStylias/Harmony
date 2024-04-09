using Harmony.Cqrs;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.Extensions.Logging;

namespace Todo.Application.Todos.Commands.CreateTodoItem;

public class CreateTodoItemCommand : Command<Result<HttpError>>
{
    private readonly ILogger<CreateTodoItemCommand> _logger;

    public CreateTodoItemCommand(ILogger<CreateTodoItemCommand> logger)
    {
        _logger = logger;
    }
    
    public override Task<Result<HttpError>> ExecuteAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}