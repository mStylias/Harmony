using Todo.Application.Auth.Common;
using Todo.Contracts.Auth.Common;
using Todo.Contracts.Todos;
using Todo.Contracts.Todos.Lists.CreateTodoList;
using Todo.Contracts.Todos.Lists.Items.CreateTodoItem;
using Todo.Domain.Entities.Todos;

namespace Todo.Api.Common.Mappers;

public static class ResponsesMapper
{
    public static AuthResponse MapToResponse(this AuthTokensModel authTokensModel)
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
}