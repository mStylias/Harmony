namespace Todo.Api.Common.Constants;

public static class EndpointBasePathNames
{
    private const string BasePath = "/api";
    
    public const string Auth = $"{BasePath}/auth";
    public const string Todos = $"{BasePath}/todos";
}