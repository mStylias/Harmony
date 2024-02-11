using Harmony.Core.Abstractions.Factories;
using Harmony.MinimalApis.Structure;
using Harmony.Test.Common;
using Harmony.Test.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Harmony.Test.Endpoints;

public class GetNameEndpoint : IEndpoint
{
    public string Tag => "Weather";
    public RouteHandlerBuilder AddEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet("/weatherforecast", async (
                ILogger<GetNameEndpoint> logger,
                [FromQuery] int id,
                [FromServices] ICohesionFabricator fabricator) =>
            {
                var query = fabricator.CreateOperation<GetNameQuery, HarmonyConfiguration>(config =>
                {
                    config.UseTransaction = true;
                });

                var request = new GetNameRequest(id);
                var result = await query.ExecuteAsync(request);
                logger.LogCritical(query.Configuration!.UseTransaction.ToString());

                return result.Error;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
}