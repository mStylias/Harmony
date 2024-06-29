namespace Todo.Contracts.Todos.Lists.CreateTodoList;

public record CreateTodoListResponse(
    int Id,
    string Name,
    string? Description
);