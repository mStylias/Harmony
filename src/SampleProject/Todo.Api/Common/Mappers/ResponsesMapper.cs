using Todo.Application.Auth.Common;
using Todo.Contracts.Auth.Common;
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

    public static CreateTodoListResponse MapToResponse(this TodoList todoList)
    {
        return new CreateTodoListResponse(todoList.Id, todoList.Name, todoList.Description);
    }
}