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

namespace Todo.Application.Todos.Commands.CreateTodoItem;

public class CreateTodoItemValidator : IHarmonyOperationValidator<CreateTodoItemCommand, Result<HttpError>>
{
    private readonly ILogger<CreateTodoItemValidator> _logger;
    private readonly ITodosRepository _todosRepository;

    public CreateTodoItemValidator(ILogger<CreateTodoItemValidator> logger,
        ITodosRepository todosRepository)
    {
        _logger = logger;
        _todosRepository = todosRepository;
    }
    
    public async Task<Result<HttpError>> ValidateAsync(CreateTodoItemCommand command, 
        CancellationToken cancellationToken = default)
    {
        // This is a debug only check to ensure that the caller of the command provides an input
        Debug.Assert(command.Input is not null, $"You must provide an input to '{nameof(CreateTodoItemCommand)}'");

        var createTodoItemRequest = command.Input;
        var validationErrors = new List<ValidationInnerError>();

        if (Enum.IsDefined(typeof(TodoStatus), (int)createTodoItemRequest.Status) == false)
        {
            validationErrors.Add(new ValidationInnerError(InnerErrorCodes.Validation.InvalidEnumValue,
                $"The status '{createTodoItemRequest.Status}' is not a valid value",
                nameof(createTodoItemRequest.Status)));
        }
        
        var nameLength = createTodoItemRequest.Name.Length;
        if (nameLength > Rules.Todos.MaximumTodoItemNameCharacters)
        {
            validationErrors.Add(new ValidationInnerError(InnerErrorCodes.Validation.MaximumCharactersExceeded,
                $"The name of the todo item exceeds the maximum of '{Rules.Todos.MaximumTodoItemNameCharacters}' " +
                "characters", nameof(createTodoItemRequest.Name)));
        }

        var todoListExists = await _todosRepository.TodoListExistsAsync(
            createTodoItemRequest.TodoListId, cancellationToken);
        if (todoListExists == false)
        {
            validationErrors.Add(new ValidationInnerError(InnerErrorCodes.Validation.EntityDoesNotExist,
                $"The todo list with id '{createTodoItemRequest.TodoListId}' " +
                $"does not exist", nameof(createTodoItemRequest.TodoListId)));
        }
        
        var nameAlreadyExists = await _todosRepository.TodoItemExistsAsync(
            createTodoItemRequest.Name, createTodoItemRequest.TodoListId, cancellationToken);
        if (nameAlreadyExists)
        {
            validationErrors.Add(new ValidationInnerError(InnerErrorCodes.Validation.EntityAlreadyExists,
                "A todo item with the same name already exists in the todo list", 
                nameof(createTodoItemRequest.Name)));
        }
        
        if (validationErrors.Count > 0)
        {
            return Errors.General.ValidationError(_logger, validationErrors);
        }

        return Result.Ok();
    }
}