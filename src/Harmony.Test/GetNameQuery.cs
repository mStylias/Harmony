using Harmony.Core;
using Harmony.Results;
using Harmony.Test.Common;
using Harmony.Test.Contracts;
using Harmony.Test.Domain.Errors;

namespace Harmony.Test;

public class GetNameQuery : Query<GetNameRequest, Result<GetNameResponse>, HarmonyConfiguration>
{
    private readonly ILogger<GetNameQuery> _logger;

    public GetNameQuery(ILogger<GetNameQuery> logger)
    {
        _logger = logger;
    }

    public override Task<Result<GetNameResponse>> ExecuteAsync(GetNameRequest input, 
        CancellationToken cancellationToken = default)
    {
        var error = Errors.BarberIdNotFound;
        error.Log();
        return Task.FromResult(Result.Fail<GetNameResponse>(error));
    }
}