namespace Todo.Contracts.Auth.Signup;

public record SignupRequest
(
    string Email,
    string Password,
    string ConfirmPassword
);