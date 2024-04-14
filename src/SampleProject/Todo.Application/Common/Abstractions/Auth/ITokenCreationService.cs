using Todo.Application.Auth.Common;

namespace Todo.Application.Common.Abstractions.Auth;

public interface ITokenCreationService
{
    /// <summary>
    /// Generates an access token and a refresh token with the given information as claims. If the refreshTokenExpiration
    /// is not provided the default value from app settings is used.
    /// </summary>
    AuthTokensModel GenerateTokens(int userId, DateTime? refreshTokenExpiration = null);
}