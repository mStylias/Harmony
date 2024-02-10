using System.Diagnostics;
using Harmony.MinimalApis.Errors;
using Harmony.Results.Abstractions;

namespace Harmony.MinimalApis.Mappers;

public static class HarmonyResultsMapper
{
    public static HttpErrorResponse MapToHttpError(this IError error)
    {
        var aspNetCoreErrorResponse = new HttpErrorResponse(
            Activity.Current?.Id,
            error.HttpStatusCode,
            error.InnerErrors);

        return aspNetCoreErrorResponse;
    }
}