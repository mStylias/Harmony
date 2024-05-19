namespace Todo.Contracts.Auth;

public record LogoutRequest(
    string RefreshToken    
);