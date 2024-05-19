using Todo.Application.Auth.Common;
using Todo.Contracts.Auth.Common;

namespace Todo.Api.Common.Mappers;

public static class RequestsMapper
{
    public static AuthResponse MapToAuthResponse(this AuthTokensModel authTokensModel)
    {
        return new AuthResponse(
            authTokensModel.RefreshToken,
            authTokensModel.AccessTokenExpiration,
            authTokensModel.RefreshTokenExpiration);
    }
}