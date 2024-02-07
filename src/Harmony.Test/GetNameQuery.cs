using Harmony.Core;
using Harmony.Results;

namespace Harmony.Test;

public class GetNameQuery : Query
{
    public int NumToReturn { get; set; }
    
    private readonly ILogger<GetNameQuery> _logger;

    public GetNameQuery(ILogger<GetNameQuery> logger)
    {
        _logger = logger;
    }

    public override async Task<Result<int>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("I was executed!!!");
        return NumToReturn;
    }
}