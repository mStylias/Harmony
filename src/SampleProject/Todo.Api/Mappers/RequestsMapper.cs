using Todo.Application.Auth.Common;
using Todo.Contracts.Auth.Signup;

namespace Todo.Api.Mappers;

public static class RequestsMapper
{
    public static SignupResponse MapToResponse(this AuthTokensModel authTokensModel)
    {
        return new SignupResponse(
            authTokensModel.RefreshToken,
            authTokensModel.AccessTokenExpiration,
            authTokensModel.RefreshTokenExpiration);
    }
}