using Harmony.Cqrs.Validators;

namespace Harmony.Test.Validators;

public class GetNameQueryValidator : IHarmonyOperationValidator<GetNameQuery, bool>
{
    public Task<bool> ValidateAsync(GetNameQuery operation, CancellationToken cancellationToken)
    {
        if (operation.Configuration.UseTransaction)
        {
            return Task.FromResult(true);
        }
        else
        {
            return Task.FromResult(false);
        }
    }
}