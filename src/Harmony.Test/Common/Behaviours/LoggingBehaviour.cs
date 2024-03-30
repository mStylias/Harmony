using Harmony.Cqrs.Abstractions;
using Harmony.MinimalApis.Errors;
using Harmony.Results.Abstractions;

namespace Harmony.Test.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IHarmonyPipelineBehaviour<TRequest, TResponse>
    where TRequest : IHarmonyOperation
    where TResponse : IResultBase<HttpError>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}