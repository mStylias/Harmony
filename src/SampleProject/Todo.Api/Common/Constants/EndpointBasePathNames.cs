namespace Todo.Api.Common.Constants;

internal static class EndpointBasePathNames
{
    public const string Auth = $"{BasePath}/auth";
    public const string Todos = $"{BasePath}/todos";
    
    private const string BasePath = "/api";
}