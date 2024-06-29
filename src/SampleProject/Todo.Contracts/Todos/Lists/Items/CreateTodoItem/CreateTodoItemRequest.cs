using Todo.Domain.Enums.Todos;

namespace Todo.Contracts.Todos.Lists.Items.CreateTodoItem;

public record CreateTodoItemRequest(
    string Name,
    string Description,
    TodoStatus Status
);