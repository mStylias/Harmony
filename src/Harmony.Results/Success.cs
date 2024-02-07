using Harmony.Results.Abstractions;

namespace Harmony.Results;

public readonly record struct Success : ISuccess
{
    public string Message { get; }
}