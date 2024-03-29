namespace Harmony.Core.Abstractions;

public interface IHarmonyOperationWithInput<TInput> : IHarmonyOperation
{
    TInput? Input { get; set; }
}