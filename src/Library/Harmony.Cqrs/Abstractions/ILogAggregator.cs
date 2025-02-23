namespace Harmony.Cqrs.Abstractions;

public interface ILogAggregator
{
    ICollection<Action> LogActions { get; }
}