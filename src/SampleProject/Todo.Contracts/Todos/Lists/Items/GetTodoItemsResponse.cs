namespace Todo.Contracts.Todos.Lists.Items;

public record GetTodoItemsResponse(
    ICollection<GetTodoItemResponse> Items);