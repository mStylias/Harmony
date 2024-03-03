using Harmony.Core.Validators;

namespace Harmony.Test.Validators;

public class GetNameQueryValidator : IHarmonyOperationValidator<GetNameQuery, bool>
{
    public Task<bool> Validate(GetNameQuery operation)
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