namespace Harmony.Results.Abstractions;

public interface ISuccess
{
    string Message { get; init; }
}

public interface ISuccess<TMetadata>
{
    string Message { get; init; }
    TMetadata Metadata { get; init; }
}