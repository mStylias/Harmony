using System.Diagnostics;
using Harmony.Cqrs.Validators;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Microsoft.Extensions.Logging;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Errors;
using Todo.Domain.Errors.Inner;

namespace Todo.Application.Todos.Lists.Items.Commands.DeleteTodoItem;

public class DeleteTodoItemValidator : IOperationValidator<DeleteTodoItemCommand, Result<HttpError>>
{
    private readonly ILogger<DeleteTodoItemValidator> _logger;
    private readonly ITodosRepository _todosRepository;

    public DeleteTodoItemValidator(
        ILogger<DeleteTodoItemValidator> logger,
        ITodosRepository todosRepository)
    {
        _logger = logger;
        _todosRepository = todosRepository;
    }
    
    public async Task<Result<HttpError>> ValidateAsync(
        DeleteTodoItemCommand operation, 
        CancellationToken cancellationToken = default)
    {
        Debug.Assert(operation.Input is not null, "You must provide a value to this command");

        var (itemId, listId, userId) = operation.Input;
        
        var listExists = await _todosRepository.TodoListExistsAsync(listId, cancellationToken).ConfigureAwait(false);
        if (listExists == false)
        {
            return DomainErrors.General.ValidationError(_logger, [
                new ValidationInnerError(
                    InnerErrorCodes.Validation.EntityDoesNotExist,
                    "List not found",
                    nameof(listId))
            ]);
        }
        
        var userOwnsList = await _todosRepository.UserOwnsListAsync(listId, userId, cancellationToken)
            .ConfigureAwait(false);
        
        if (userOwnsList == false)
        {
            return DomainErrors.General.ValidationError(_logger, [
                new ValidationInnerError(
                    InnerErrorCodes.Validation.NoPermission,
                    "This list doesn't belong to the logged in user",
                    null)
            ]);
        }

        return Result.Ok();
    }
}