namespace Todo.Contracts.Todos.CreateTodoList;

public record CreateTodoListRequest(
    string Name,
    string? Description
);