namespace Todo.Contracts.Todos.CreateTodoList;

public record CreateTodoListResponse(
    int Id,
    string Name,
    string? Description
);