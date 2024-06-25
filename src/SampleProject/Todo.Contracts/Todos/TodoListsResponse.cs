namespace Todo.Contracts.Todos;

public record TodoListsResponse(
    int Id,
    string Name,
    string? Description
);