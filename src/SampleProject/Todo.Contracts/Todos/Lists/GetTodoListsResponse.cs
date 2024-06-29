namespace Todo.Contracts.Todos.Lists;

public record GetTodoListsResponse(
    List<GetTodoListResponse> TodoLists
);