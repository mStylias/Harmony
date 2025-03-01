using Harmony.Cqrs.Operations;
using Harmony.Cqrs.Validators;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.Extensions.Logging;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Entities.Todos;
using Todo.Domain.Enums.Todos;

namespace Todo.Application.Todos.Lists.Items.Commands.CreateTodoItem;

public class CreateTodoItemCommand : Command<Result<TodoItem, HttpError>>
{
    private readonly ILogger<CreateTodoItemCommand> _logger;
    private readonly IOperationValidator<CreateTodoItemCommand, Result<HttpError>> _validator;
    private readonly ITodosRepository _todosRepository;

    public CreateTodoItemCommand(
        ILogger<CreateTodoItemCommand> logger, 
        IOperationValidator<CreateTodoItemCommand, Result<HttpError>> validator,
        ITodosRepository todosRepository)
    {
        _logger = logger;
        _validator = validator;
        _todosRepository = todosRepository;
    }

    public string TodoName { get; set; } = null!;
    public string TodoDescription { get; set; } = null!;
    public TodoStatus TodoStatus { get; set; }
    public int TodoListId { get; set; }
    public string UserId { get; set; } = null!;

    public override async Task<Result<TodoItem, HttpError>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(this, cancellationToken).ConfigureAwait(false);
        if (validationResult.IsError)
        {
            return validationResult.Error;
        }

        var todoItem = new TodoItem(
            TodoName, 
            TodoDescription, 
            TodoStatus,
            TodoListId);

        await _todosRepository.CreateTodoItemAsync(todoItem).ConfigureAwait(false);

        _logger.LogInformation("Successfully created todo item with id {TodoItemId}", todoItem.Id);
        
        return todoItem;
    }
}