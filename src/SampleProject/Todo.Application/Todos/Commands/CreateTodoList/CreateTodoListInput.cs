namespace Todo.Application.Todos.Commands.CreateTodoList;

public record CreateTodoListInput(
    string UserId,
    string Name,
    string? Description
);