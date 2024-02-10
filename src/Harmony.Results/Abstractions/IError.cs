namespace Harmony.Results.Abstractions;

public interface IError
{
    int HttpStatusCode { get; }
    List<InnerError> InnerErrors { get; }
    void Log();
}