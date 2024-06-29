using Dapper;
using Todo.Application.Common.Abstractions.Repositories;
using Todo.Domain.Entities.Todos;

namespace Todo.Infrastructure.Persistence.Repositories;

public class TodosRepository : ITodosRepository
{
    private readonly DapperDbContext _dbContext;

    public TodosRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<TodoList?> GetTodoListById(int todoListId, CancellationToken cancellationToken = default)
    {
        using var connection = _dbContext.CreateConnection();
        var todoList = connection.QueryFirstOrDefaultAsync<TodoList>(new CommandDefinition(
            "SELECT * FROM todo_lists WHERE id=@todoListId", 
            new { todoListId }, cancellationToken: cancellationToken));

        return todoList;
    }

    public async Task<IEnumerable<TodoList>> GetTodoListsOfUserAsync(string userId, CancellationToken cancellationToken)
    {
        using var connection = _dbContext.CreateConnection();
        var todoLists = await connection.QueryAsync<TodoList>(new CommandDefinition(
            "SELECT * FROM todo_lists WHERE user_id=@userId", 
            new { userId }, cancellationToken: cancellationToken));
        
        return todoLists;
    }
    
    public async Task<IEnumerable<TodoItem>> GetTodoListItemsAsync(int todoListId, CancellationToken cancellationToken)
    {
        using var connection = _dbContext.CreateConnection();
        var todoItems = await connection.QueryAsync<TodoItem>(new CommandDefinition(
            "SELECT * FROM todo_items WHERE todo_list_id=@todoListId", 
            new { todoListId }, cancellationToken: cancellationToken));
        
        return todoItems;
    }

    public Task<IEnumerable<TodoItem>> GetTodoItemsOfMultipleListsAsync(IEnumerable<int> todoListIds, 
        CancellationToken cancellationToken)
    {
        using var connection = _dbContext.CreateConnection();
        var todoItems = connection.QueryAsync<TodoItem>(
            new CommandDefinition("SELECT * FROM todo_items WHERE todo_list_id in @todoListIds", 
                new { todoListIds }, cancellationToken: cancellationToken));

        return todoItems;
    }

    public async Task<TodoList> CreateTodoListAsync(TodoList todoList)
    {
        using var connection = _dbContext.CreateConnection();
        var todoListId = await connection.ExecuteScalarAsync<int>(
            "INSERT INTO todo_lists (name, description, user_id) " +
            "VALUES (@Name, @Description, @UserId) RETURNING id",
            todoList);
        
        todoList.Id = todoListId;

        return todoList;
    }

    public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
    {
        using var connection = _dbContext.CreateConnection();
        var todoItemId = await connection.ExecuteScalarAsync<int>(
            "INSERT INTO todo_items (name, description, status, todo_list_id) " +
            "VALUES (@Name, @Description, @Status, @TodoListId) RETURNING id",
            todoItem);
        
        todoItem.Id = todoItemId;
        
        return todoItem;
    }

    public async Task<bool> TodoListExistsAsync(string name, string userId, CancellationToken cancellationToken)
    {
        using var connection = _dbContext.CreateConnection();
        var todoList = await connection.QueryFirstOrDefaultAsync<int>(new CommandDefinition(
            "SELECT 1 FROM todo_lists WHERE name=@name AND user_id=@userId LIMIT 1",
            new { name, userId }, cancellationToken: cancellationToken));

        return todoList == 1;
    }

    public async Task<bool> TodoListExistsAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = _dbContext.CreateConnection();
        var todoList = await connection.QueryFirstOrDefaultAsync<int>(new CommandDefinition(
            "SELECT 1 FROM todo_lists WHERE id=@id LIMIT 1",
            new { id }, cancellationToken: cancellationToken));
        
        return todoList == 1;
    }
    
    public async Task<bool> TodoItemExistsAsync(string name, int todoListId, CancellationToken cancellationToken)
    {
        using var connection = _dbContext.CreateConnection();
        var todoItem = await connection.QueryFirstOrDefaultAsync<int>(new CommandDefinition(
            "SELECT 1 FROM todo_items WHERE name=@name AND todo_list_id=@todoListId LIMIT 1",
            new { name, todoListId }, cancellationToken: cancellationToken));

        return todoItem == 1;
    }
}