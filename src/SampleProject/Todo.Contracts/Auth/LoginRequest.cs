namespace Todo.Contracts.Auth;

public record LoginRequest(
    string Email,
    string Password
);