using Todo.Application.Auth.Common;
using Todo.Contracts.Auth.Common;
using Todo.Contracts.Todos.Lists;
using Todo.Contracts.Todos.Lists.CreateTodoList;
using Todo.Contracts.Todos.Lists.Items;
using Todo.Contracts.Todos.Lists.Items.CreateTodoItem;
using Todo.Domain.Entities.Todos;

namespace Todo.Api.Common.Mappers;

public static class ResponsesMapper
{
    public static AuthResponse MapToAuthResponse(this AuthTokensModel authTokensModel)
    {
        return new AuthResponse(
            authTokensModel.RefreshToken,
            authTokensModel.AccessTokenExpiration,
            authTokensModel.RefreshTokenExpiration);
    }

    public static CreateTodoListResponse MapToCreateTodoListResponse(this TodoList todoList)
    {
        return new CreateTodoListResponse(todoList.Id, todoList.Name, todoList.Description);
    }
    
    public static CreateTodoItemResponse MapToCreateTodoItemResponse(this TodoItem todoItem)
    {
        return new CreateTodoItemResponse(todoItem.Id, todoItem.Name, todoItem.Description, todoItem.Status, 
            todoItem.TodoListId);
    }

    public static GetTodoListResponse MapToGetTodoListResponse(this TodoList todoList)
    {
        return new GetTodoListResponse(todoList.Id, todoList.Name, todoList.Description);
    }
    
    public static GetTodoListsResponse MapToGetTodoListsResponse(this IEnumerable<TodoList> todoLists)
    {
        return new GetTodoListsResponse(todoLists.Select(list => 
                list.MapToGetTodoListResponse())
            .ToList()
        );
    }
    
    public static GetTodoItemResponse MapToGetTodoItemResponse(this TodoItem todoItem)
    {
        return new GetTodoItemResponse(todoItem.Id, todoItem.Name, todoItem.Description, todoItem.Status);
    }
    
    public static GetTodoItemsResponse MapToGetTodoItemsResponse(this IEnumerable<TodoItem> todoItems)
    {
        return new GetTodoItemsResponse(todoItems.Select(list => 
                list.MapToGetTodoItemResponse())
            .ToList()
        );
    }
}