using Harmony.Cqrs;
using Harmony.Cqrs.Validators;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.Extensions.Logging;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Contracts.Todos.CreateTodoItem;
using Todo.Domain.Entities.Todos;

namespace Todo.Application.Todos.Commands.CreateTodoItem;

public class CreateTodoItemCommand : Command<CreateTodoItemRequest, Result<TodoItem, HttpError>>
{
    private readonly ILogger<CreateTodoItemCommand> _logger;
    private readonly IHarmonyOperationValidator<CreateTodoItemCommand, Result<HttpError>> _validator;
    private readonly ITodosRepository _todosRepository;

    public CreateTodoItemCommand(ILogger<CreateTodoItemCommand> logger, 
        IHarmonyOperationValidator<CreateTodoItemCommand, Result<HttpError>> validator,
        ITodosRepository todosRepository)
    {
        _logger = logger;
        _validator = validator;
        _todosRepository = todosRepository;
    }

    public override CreateTodoItemRequest? Input { get; set; }

    public override async Task<Result<TodoItem, HttpError>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(this, cancellationToken);
        if (validationResult.IsError)
        {
            return validationResult.Error;
        }
        
        var createTodoItemRequest = Input!;
        
        var todoItem = new TodoItem(
            createTodoItemRequest.Name, 
            createTodoItemRequest.Description, 
            createTodoItemRequest.Status,
            createTodoItemRequest.TodoListId);

        await _todosRepository.CreateTodoItemAsync(todoItem);

        _logger.LogInformation("Successfully created todo item with id {TodoItemId}", todoItem.Id);
        
        return todoItem;
    }
}