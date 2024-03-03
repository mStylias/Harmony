using Harmony.Core.Abstractions;

namespace Harmony.Core.Validators;

public interface IHarmonyOperationValidator<in TOperation, TOutput> where TOperation : IHarmonyOperation
{
    public Task<TOutput> Validate(TOperation operation);
}