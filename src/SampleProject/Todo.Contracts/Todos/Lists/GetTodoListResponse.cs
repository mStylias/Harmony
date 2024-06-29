namespace Todo.Contracts.Todos.Lists;

public record GetTodoListResponse(
    int Id,
    string Name,
    string? Description
);