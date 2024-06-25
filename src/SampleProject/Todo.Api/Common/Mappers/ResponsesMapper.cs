using Todo.Application.Auth.Common;
using Todo.Contracts.Auth.Common;
using Todo.Contracts.Todos;
using Todo.Contracts.Todos.CreateTodoList;
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
    
    public static TodoListsResponse MapToTodoListResponse(this TodoList todoList)
    {
        return new TodoListsResponse(todoList.Id, todoList.Name, todoList.Description);
    }
}