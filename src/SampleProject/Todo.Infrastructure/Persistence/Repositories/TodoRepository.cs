using Dapper;
using Todo.Domain.Entities.Todos;

namespace Todo.Infrastructure.Persistence.Repositories;

public class TodoRepository
{
    private readonly DapperDbContext _dbContext;

    public TodoRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<TodoItem>> GetTodoListItemsAsync(int todoListId)
    {
        using var connection = _dbContext.CreateConnection();
        var todoItems = await connection.QueryAsync<TodoItem>(
            "SELECT * FROM todo_items WHERE todo_id=@todoListId", 
            new { todoListId });
        return todoItems;
    }
}