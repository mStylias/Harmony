namespace Todo.Contracts.Auth.Signup;

public record SignupResponse
(
    string RefreshToken,
    DateTime AccessTokenExpiration,
    DateTime RefreshTokenExpiration    
);