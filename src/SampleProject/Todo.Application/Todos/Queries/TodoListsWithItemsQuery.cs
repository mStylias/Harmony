using System.Diagnostics;
using Harmony.Cqrs;
using Harmony.MinimalApis.Errors;
using Harmony.Results;
using Harmony.Results.ErrorTypes.InnerErrorTypes;
using Microsoft.Extensions.Logging;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Contracts.Todos;
using Todo.Contracts.Todos.Lists.Items;
using Todo.Domain.Errors;
using Todo.Domain.Errors.Inner;

namespace Todo.Application.Todos.Queries;

/// <summary>
/// Fetches all todo lists with their items for the user with the given id.
/// </summary>
public class TodoListsWithItemsQuery : Query<string?, Result<TodoListsWithItemsResponse, HttpError>>
{
    private readonly ILogger<TodoListsWithItemsQuery> _logger;
    private readonly ITodosRepository _todosRepository;

    public TodoListsWithItemsQuery(
        ILogger<TodoListsWithItemsQuery> logger,
        ITodosRepository todosRepository)
    {
        _logger = logger;
        _todosRepository = todosRepository;
    }
    
    public override string? Input { get; set; }

    public override async Task<Result<TodoListsWithItemsResponse, HttpError>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        Debug.Assert(Input is not null, "You must provide a user id to fetch todo lists with items");
        
        var userId = Input;

        var todoLists = (await _todosRepository
            .GetTodoListsOfUserAsync(userId, cancellationToken))
            .ToArray();

        if (todoLists.Length == 0)
        {
            return Errors.General.ValidationError(_logger, [
                new ValidationInnerError(InnerErrorCodes.Validation.EntityDoesNotExist,
                    "No todo lists found for the given user id",
                    nameof(todoLists))
            ]);
        }
        
        var allTodoItems = await _todosRepository
            .GetTodoItemsOfMultipleListsAsync(todoLists.Select(x => x.Id), cancellationToken);

        var todoListsWithItems = new TodoListsWithItemsResponse(
            todoLists.Select(list => new TodoListWithItemsResponse(
                list.Id, 
                list.Name, 
                list.Description, 
                allTodoItems
                    .Where(item => item.TodoListId == list.Id)
                    .Select(item => new GetTodoItemResponse(
                        item.Id,
                        item.Name,
                        item.Description,
                        item.Status)
                    )
                    .ToList()
                )
            )
            .ToList()
        );

        return todoListsWithItems;
    }
}