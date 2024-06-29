using Harmony.Cqrs.Abstractions;

namespace Harmony.Cqrs.Validators;

public interface IOperationValidator<in TOperation, TOutput> where TOperation : IHarmonyOperation
{
    public Task<TOutput> ValidateAsync(TOperation operation, CancellationToken cancellation = default);
}