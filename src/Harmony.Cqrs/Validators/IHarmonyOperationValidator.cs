using Harmony.Cqrs.Abstractions;

namespace Harmony.Cqrs.Validators;

public interface IHarmonyOperationValidator<in TOperation, TOutput> where TOperation : IHarmonyOperation
{
    public Task<TOutput> ValidateAsync(TOperation operation);
}