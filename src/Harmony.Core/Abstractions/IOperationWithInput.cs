namespace Harmony.Core.Abstractions;

public interface IOperationWithInput<TInput> : IHarmonyOperation
{
    TInput? Input { get; set; }
}