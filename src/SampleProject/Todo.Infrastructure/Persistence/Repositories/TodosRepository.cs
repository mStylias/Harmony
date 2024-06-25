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
    
    public async Task<IEnumerable<TodoList>> GetTodoListsOfUserAsync(string userId)
    {
        using var connection = _dbContext.CreateConnection();
        var todoLists = await connection.QueryAsync<TodoList>(
            "SELECT * FROM todo_lists WHERE user_id=@userId", 
            new { userId });
        
        return todoLists;
    }
    
    public async Task<IEnumerable<TodoItem>> GetTodoListItemsAsync(int todoListId)
    {
        using var connection = _dbContext.CreateConnection();
        var todoItems = await connection.QueryAsync<TodoItem>(
            "SELECT * FROM todo_items WHERE todo_id=@todoListId", 
            new { todoListId });
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

    public async Task<bool> TodoListExistsAsync(string name, string userId)
    {
        using var connection = _dbContext.CreateConnection();
        var todoList = await connection.QueryFirstOrDefaultAsync<int>(
            "SELECT 1 FROM todo_lists WHERE name=@name AND user_id=@userId",
            new { name, userId });

        return todoList == 1;
    }
}