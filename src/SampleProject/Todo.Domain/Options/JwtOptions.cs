namespace Todo.Domain.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    
    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Subject { get; init; }
    public required TimeSpan ExpirationTime { get; init; }
}