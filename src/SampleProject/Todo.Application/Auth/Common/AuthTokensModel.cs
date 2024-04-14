namespace Todo.Application.Auth.Common;

public record AuthTokensModel
(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiration,
    DateTime RefreshTokenExpiration
);