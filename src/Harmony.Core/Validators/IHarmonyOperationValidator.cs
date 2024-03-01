using Harmony.Core.Abstractions;
using Harmony.Results;

namespace Harmony.Core.Validators;

public interface IHarmonyOperationValidator<TOperation> where TOperation : IHarmonyOperation
{
    public Task<Result> Validate<TModel>(TModel model);
}