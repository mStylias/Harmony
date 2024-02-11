using Harmony.Results.Abstractions;

namespace Harmony.Results;

public class Success : ISuccess
{
    public Success(string message)
    {
        Message = message;
    }
    
    public string Message { get; init; }
}

public class Success<TMetadata> : ISuccess
{
    public string Message { get; init; }
    public TMetadata Metadata { get; init; }

    public Success(string message, TMetadata metadata)
    {
        Message = message;
        Metadata = metadata;
    }
}