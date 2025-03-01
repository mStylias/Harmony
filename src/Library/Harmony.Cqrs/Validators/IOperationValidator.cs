using Harmony.Cqrs.Abstractions;

namespace Harmony.Cqrs.Validators;

public interface IOperationValidator<in TOperation, TOutput> 
    where TOperation : IOperationBase
{
    public Task<TOutput> ValidateAsync(TOperation operation, CancellationToken cancellationToken = default);
}