namespace Todo.Contracts.Todos.Lists;

public record GetTodoListsResponse(
    ICollection<GetTodoListResponse> TodoLists);