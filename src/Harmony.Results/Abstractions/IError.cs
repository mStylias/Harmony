namespace Harmony.Results.Abstractions;

public interface IError
{
    ErrorType Type { get; }
    int HttpStatusCode { get; }
    List<InnerError>? InnerErrors { get; }
    void Log();
}