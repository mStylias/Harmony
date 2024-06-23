using Todo.Domain.Entities.Todos;

namespace Todo.Application.Common.Abstractions.Repositories;

public interface ITodosRepository
{
    Task<IEnumerable<TodoItem>> GetTodoListItemsAsync(int todoListId);
    Task<TodoList> CreateTodoListAsync(TodoList todoList);
    Task<bool> TodoListExistsAsync(string name, string userId);
}