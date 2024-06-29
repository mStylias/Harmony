using Todo.Domain.Enums.Todos;

namespace Todo.Contracts.Todos.Lists.Items.CreateTodoItem;

public record CreateTodoItemResponse(
    int Id,
    string Name,
    string Description,
    TodoStatus Status,
    int TodoListId);