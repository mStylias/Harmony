namespace Todo.Contracts.Todos.Lists.CreateTodoList;

public record CreateTodoListRequest(
    string Name,
    string? Description
);