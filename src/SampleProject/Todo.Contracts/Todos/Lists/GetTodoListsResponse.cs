using Todo.Contracts.Todos.Lists.Items;

namespace Todo.Contracts.Todos.Lists;

public record GetTodoListsResponse(
    List<GetTodoListResponse> TodoLists
);