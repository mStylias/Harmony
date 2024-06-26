using Todo.Domain.Enums.Todos;

namespace Todo.Contracts.Todos.CreateTodoItem;

public record CreateTodoItemRequest(
    string Name,
    string Description,
    TodoStatus Status,
    int TodoListId
);