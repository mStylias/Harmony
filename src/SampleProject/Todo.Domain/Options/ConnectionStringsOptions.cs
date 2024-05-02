namespace Todo.Domain.Options;

public class ConnectionStringsOptions
{
    public const string SectionName = "ConnectionStrings";

    public required string Default { get; init; }
}