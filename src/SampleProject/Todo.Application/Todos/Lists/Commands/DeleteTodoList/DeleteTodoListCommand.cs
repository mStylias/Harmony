using Harmony.Cqrs;
using Harmony.Cqrs.Validators;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Todo.Application.Common.Abstractions.Repositories;

namespace Todo.Application.Todos.Lists.Commands.DeleteTodoList;

public class DeleteTodoListCommand : Command<DeleteTodoListInput, Result<HttpError>>
{
    private readonly IOperationValidator<DeleteTodoListCommand, Result<HttpError>> _validator;
    private readonly ITodosRepository _todosRepository;

    public DeleteTodoListCommand(IOperationValidator<DeleteTodoListCommand, Result<HttpError>> validator,
        ITodosRepository todosRepository)
    {
        _validator = validator;
        _todosRepository = todosRepository;
    }
    
    public override DeleteTodoListInput? Input { get; set; }

    public override async Task<Result<HttpError>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(this, cancellationToken);
        if (validationResult.IsError)
        {
            return validationResult.Error;
        }

        await _todosRepository.DeleteTodoList(Input!.ListId);

        return Result.Ok();
    }
}