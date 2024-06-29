using Harmony.Cqrs;
using Harmony.Cqrs.Validators;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Microsoft.Extensions.Logging;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Entities.Todos;

namespace Todo.Application.Todos.Lists.Commands.CreateTodoList;

public class CreateTodoListCommand : Command<CreateTodoListInput, Result<TodoList, HttpError>>
{
    private readonly ILogger<CreateTodoListCommand> _logger;
    private readonly IOperationValidator<CreateTodoListCommand, Result<HttpError>> _validator;
    private readonly ITodosRepository _todosRepository;

    public CreateTodoListCommand(ILogger<CreateTodoListCommand> logger,
        IOperationValidator<CreateTodoListCommand, Result<HttpError>> validator,
        ITodosRepository todosRepository)
    {
        _logger = logger;
        _validator = validator;
        _todosRepository = todosRepository;
    }

    public override CreateTodoListInput? Input { get; set; }

    public override async Task<Result<TodoList, HttpError>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(this, cancellationToken);
        if (validationResult.IsError)
        {
            return validationResult.Error;
        }
        
        var todoList = new TodoList(Input!.Name, Input.Description, Input.UserId);
        
        var createdTodoList = await _todosRepository.CreateTodoListAsync(todoList);

        _logger.LogInformation("Successfully created todo list with id '{TodoListId}'", createdTodoList.Id);
        
        return createdTodoList;
    }
}