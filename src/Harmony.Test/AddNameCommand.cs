using Harmony.Core;
using Harmony.Results;

namespace Harmony.Test;

public class AddNameCommand : Command<Unit, Result>
{
    public override Result Execute(Unit input, CancellationToken cancellationToken = default)
    {
        var success = new Success("Ta kataferame, we did it!");
        return Result.Ok(success: success);
    }    
}