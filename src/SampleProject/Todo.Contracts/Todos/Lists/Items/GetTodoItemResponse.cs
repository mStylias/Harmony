using Todo.Domain.Enums.Todos;

namespace Todo.Contracts.Todos.Lists.Items;

public record GetTodoItemResponse(
    int Id,
    string Name,
    string Description,
    TodoStatus Status
);