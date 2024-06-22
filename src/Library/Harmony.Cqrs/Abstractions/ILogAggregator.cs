namespace Harmony.Cqrs.Abstractions;

public interface ILogAggregator
{
    List<Action> LogActions { get; }
}