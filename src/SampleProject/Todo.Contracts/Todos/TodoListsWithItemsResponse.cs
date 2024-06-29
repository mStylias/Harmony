using Todo.Contracts.Todos.Lists;
using Todo.Contracts.Todos.Lists.Items;

namespace Todo.Contracts.Todos;

public record TodoListWithItemsResponse(int Id, string Name, string? Description, List<GetTodoItemResponse> Items) 
    : GetTodoListResponse(Id, Name, Description);

public record TodoListsWithItemsResponse(
    List<TodoListWithItemsResponse> TodoLists
);