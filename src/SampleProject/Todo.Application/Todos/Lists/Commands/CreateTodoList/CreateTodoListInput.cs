namespace Todo.Application.Todos.Lists.Commands.CreateTodoList;

public record CreateTodoListInput(
    string UserId,
    string Name,
    string? Description
);