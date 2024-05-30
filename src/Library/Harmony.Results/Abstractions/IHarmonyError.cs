using Harmony.Results.Enums;

namespace Harmony.Results.Abstractions;

public interface IHarmonyError
{
    Severity Severity { get; }
}