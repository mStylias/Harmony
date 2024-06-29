namespace Todo.Application.Todos.Lists.Commands.DeleteTodoList;

public record DeleteTodoListInput(
    int ListId,
    string UserId
);