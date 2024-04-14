using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Harmony.MinimalApis.Structure;

/// <summary>
/// This interface represents one endpoint of the application. When AddApiEndpoints is called from IServiceCollection
/// all types that implement this interface will be registered and added as api endpoints when MapApiEndpoints
/// of WebApplicationBuilder is called
/// </summary>
public interface IEndpoint
{
    public string Tag { get; }
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app);
}