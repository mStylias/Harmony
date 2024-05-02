namespace Todo.Domain.Options;

public class RefreshTokenOptions
{
    public const string SectionName = "RefreshToken";
    
    public required string Key { get; init; }
    public required TimeSpan ExpirationTime { get; init; }
}