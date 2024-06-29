namespace Todo.Contracts.Todos.Lists.Items;

public record GetTodoItemsResponse(
    List<GetTodoItemResponse> Items
);