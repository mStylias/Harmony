using Harmony.MinimalApis.Errors;
using Harmony.Results;

namespace Harmony.Test.Domain.Errors;

public static partial class Errors
{
    public static HttpError BarberIdNotFound(ILogger logger) => new (
        "BARBER_ID_NOT_FOUND",
        "Barber id not found",
        StatusCodes.Status404NotFound, 
        () => logger.LogError("Barber id not found"));
}