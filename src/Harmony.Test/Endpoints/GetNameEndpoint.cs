using Harmony.Core.Abstractions;
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
        return app.MapGet("/weatherforecast", async Task<IResult>(
                ILogger<GetNameEndpoint> logger,
                [FromQuery] int id,
                [FromServices] IHarmonicon harmonicon) =>
            {
                var testCompilerWarningQuery = harmonicon.SynthesizeOperation<GetNameQuery>();
                var testCompilerWarningQuery2 = harmonicon.SynthesizeOperation<GetNameQuery, GetNameRequest>(new GetNameRequest(7));
                
                var command = harmonicon.SynthesizeOperation<AddNameCommand>();
                var commandResult = await command.ExecuteAsync();
                
                if (commandResult.IsError)
                {
                    return Microsoft.AspNetCore.Http.Results.BadRequest(commandResult.Error);
                }
                
                var request = new GetNameRequest(id);
                
                var query = harmonicon.SynthesizeOperation<GetNameQuery, GetNameRequest, HarmonyConfiguration>(
                     request, config =>
                {
                    config.UseTransaction = true;
                });
                
                var nameResponse = await query.ExecuteAsync();
                return nameResponse.IsSuccess switch
                {
                    true => Microsoft.AspNetCore.Http.Results.Ok(nameResponse.Value!),
                    false => Microsoft.AspNetCore.Http.Results.BadRequest(nameResponse.Error)
                };
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
}