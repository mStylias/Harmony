using Harmony.Cqrs;
using Harmony.MinimalApis.Errors;
using Harmony.Results;

namespace Harmony.Test;

public class AddNameCommand : Command<Result<HttpError>>
{
    public override async Task<Result<HttpError>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var success = new Success("Ta kataferame, we did it!");
        return Result.Ok();
    }
}