using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Harmony.MinimalApis.Structure;

public interface IEndpoint
{
    public string Tag { get; }
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app);
}