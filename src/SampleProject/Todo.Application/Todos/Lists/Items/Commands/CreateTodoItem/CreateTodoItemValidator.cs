using System.Diagnostics;
using Harmony.Cqrs.Validators;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Microsoft.Extensions.Logging;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Enums.Todos;
using Todo.Domain.Errors;
using Todo.Domain.Errors.Inner;
using Todo.Domain.Rules;

namespace Todo.Application.Todos.Lists.Items.Commands.CreateTodoItem;

public class CreateTodoItemValidator : IOperationValidator<CreateTodoItemCommand, Result<HttpError>>
{
    private readonly ILogger<CreateTodoItemValidator> _logger;
    private readonly ITodosRepository _todosRepository;

    public CreateTodoItemValidator(
        ILogger<CreateTodoItemValidator> logger,
        ITodosRepository todosRepository)
    {
        _logger = logger;
        _todosRepository = todosRepository;
    }
    
    public async Task<Result<HttpError>> ValidateAsync(
        CreateTodoItemCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationErrors = new List<ValidationInnerError>();

        if (Enum.IsDefined(typeof(TodoStatus), (int)command.TodoStatus) == false)
        {
            validationErrors.Add(new ValidationInnerError(
                InnerErrorCodes.Validation.InvalidEnumValue,
                $"The status '{command.TodoStatus}' is not a valid value",
                nameof(command.TodoStatus)));
        }
        
        var nameLength = command.TodoName.Length;
        if (nameLength > DomainRules.Todos.MaximumTodoItemNameCharacters)
        {
            validationErrors.Add(new ValidationInnerError(
                InnerErrorCodes.Validation.MaximumCharactersExceeded,
                $"The name of the todo item exceeds the maximum of '{DomainRules.Todos.MaximumTodoItemNameCharacters}' " +
                "characters", 
                nameof(command.TodoName)));
        }
        
        var todoList = await _todosRepository.GetTodoListById(
            command.TodoListId, 
            cancellationToken).ConfigureAwait(false);
        
        if (todoList is null)
        {
            validationErrors.Add(new ValidationInnerError(
                InnerErrorCodes.Validation.EntityDoesNotExist,
                $"The todo list with id '{command.TodoListId}' " +
                $"does not exist", 
                nameof(command.TodoListId)));
        }

        if (todoList is not null && todoList.UserId != command.UserId)
        {
            validationErrors.Add(new ValidationInnerError(
                InnerErrorCodes.Validation.NoPermission,
                $"The todo list with id '{command.TodoListId}' " +
                $"doesn't belong to this user", 
                nameof(command.TodoListId)));
        }
        
        var nameAlreadyExists = await _todosRepository.TodoItemExistsAsync(
            command.TodoName, 
            command.TodoListId, 
            cancellationToken).ConfigureAwait(false);
        
        if (nameAlreadyExists)
        {
            validationErrors.Add(new ValidationInnerError(
                InnerErrorCodes.Validation.EntityAlreadyExists,
                "A todo item with the same name already exists in the todo list", 
                nameof(command.TodoName)));
        }
        
        if (validationErrors.Count > 0)
        {
            return DomainErrors.General.ValidationError(_logger, validationErrors);
        }

        return Result.Ok();
    }
}