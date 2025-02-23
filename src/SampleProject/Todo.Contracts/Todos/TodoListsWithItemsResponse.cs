using Todo.Contracts.Todos.Lists;
using Todo.Contracts.Todos.Lists.Items;

namespace Todo.Contracts.Todos;

public record TodoListsWithItemsResponse(
    ICollection<TodoListWithItemsResponse> TodoLists);

public record TodoListWithItemsResponse(int Id, string Name, string? Description, ICollection<GetTodoItemResponse> Items) 
    : GetTodoListResponse(Id, Name, Description);