using Harmony.Cqrs;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.Extensions.Logging;

namespace Todo.Application.Todos.Commands.DeleteTodoItem;

public class DeleteTodoItemCommand : Command<Result<HttpError>>
{
    private readonly ILogger<DeleteTodoItemCommand> _logger;

    public DeleteTodoItemCommand(ILogger<DeleteTodoItemCommand> logger)
    {
        _logger = logger;
    }
    
    public override Task<Result<HttpError>> ExecuteAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}