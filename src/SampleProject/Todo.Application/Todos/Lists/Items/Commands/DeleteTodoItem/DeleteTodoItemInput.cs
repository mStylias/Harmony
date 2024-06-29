namespace Todo.Application.Todos.Lists.Items.Commands.DeleteTodoItem;

public record DeleteTodoItemInput(
    int ItemId,
    int ListId,
    string UserId
);