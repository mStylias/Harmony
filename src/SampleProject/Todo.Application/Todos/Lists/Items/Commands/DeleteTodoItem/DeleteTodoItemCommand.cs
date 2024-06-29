using Harmony.Cqrs;
using Harmony.Cqrs.Validators;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Microsoft.Extensions.Logging;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Errors;
using Todo.Domain.Errors.Inner;

namespace Todo.Application.Todos.Lists.Items.Commands.DeleteTodoItem;

public class DeleteTodoItemCommand : Command<DeleteTodoItemInput, Result<HttpError>>
{
    private readonly ILogger<DeleteTodoItemCommand> _logger;
    private readonly IOperationValidator<DeleteTodoItemCommand, Result<HttpError>> _validator;
    private readonly ITodosRepository _todosRepository;

    public DeleteTodoItemCommand(ILogger<DeleteTodoItemCommand> logger,
        IOperationValidator<DeleteTodoItemCommand, Result<HttpError>> validator,
        ITodosRepository todosRepository)
    {
        _logger = logger;
        _validator = validator;
        _todosRepository = todosRepository;
    }

    public override DeleteTodoItemInput? Input { get; set; }

    public override async Task<Result<HttpError>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(this, cancellationToken);
        if (validationResult.IsError)
        {
            return validationResult.Error;
        }

        var itemId = Input!.ItemId;

        int rowsAffectedNum = await _todosRepository.DeleteTodoItem(itemId);
        if (rowsAffectedNum == 0)
        {
            return Errors.General.ValidationError(_logger, [
                new ValidationInnerError(
                    InnerErrorCodes.Validation.EntityDoesNotExist,
                    "Item not found",
                    nameof(itemId))
                ]);
        }

        return Result.Ok();
    }
}