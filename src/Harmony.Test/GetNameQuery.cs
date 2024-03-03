using Harmony.Core;
using Harmony.Core.Abstractions;
using Harmony.Results;
using Harmony.Test.Common;
using Harmony.Test.Contracts;

namespace Harmony.Test;

public class GetNameQuery : Query<GetNameRequest, Result<GetNameResponse>>, IConfigurable<HarmonyConfiguration>
{
    public HarmonyConfiguration Configuration { get; set; }
    public override GetNameRequest? Input { get; set; }
    
    private readonly ILogger<GetNameQuery> _logger;

    public GetNameQuery(ILogger<GetNameQuery> logger)
    {
        _logger = logger;
    }
    
    public override async Task<Result<GetNameResponse>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Use transaction value: {ConfigValue}", Configuration.UseTransaction);
        _logger.LogInformation("Input id: {InputId}", Input!.Id);
        return new GetNameResponse("Jack");
    }
}