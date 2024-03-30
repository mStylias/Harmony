namespace Harmony.Cqrs.Abstractions;

public interface IHarmonyOperationWithInput<TInput> : IHarmonyOperation
{
    TInput? Input { get; set; }
}