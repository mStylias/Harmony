using Todo.Domain.Enums.Todos;

namespace Todo.Application.Todos.Lists.Items.Commands.CreateTodoItem;

public record CreateTodoItemInput(
    string Name,
    string Description,
    TodoStatus Status,
    int TodoListId,
    string UserId
);