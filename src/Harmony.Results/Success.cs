using Harmony.Results.Abstractions;

namespace Harmony.Results;

public readonly record struct Success : ISuccess
{
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