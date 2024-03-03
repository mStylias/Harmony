using Harmony.Core;
using Harmony.Results;

namespace Harmony.Test;

public class AddNameCommand : Command<Result>
{
    public override async Task<Result> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var success = new Success("Ta kataferame, we did it!");
        return Result.Ok(success: success);
    }
}