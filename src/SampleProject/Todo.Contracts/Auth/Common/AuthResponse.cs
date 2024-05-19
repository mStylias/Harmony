namespace Todo.Contracts.Auth.Common;

public record AuthResponse
(
    string RefreshToken,
    DateTime AccessTokenExpiration,
    DateTime RefreshTokenExpiration    
);