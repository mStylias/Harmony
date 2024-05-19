namespace Todo.Contracts.Auth;

public record SignupRequest
(
    string Email,
    string Password,
    string ConfirmPassword
);