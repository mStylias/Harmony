using Harmony.Results;

namespace Harmony.Test.Domain.Errors;

public static partial class Errors
{
    public static Error BarberIdNotFound => ErrorFactory.CreateError(
        "BARBER_ID_NOT_FOUND",
        "Barber id not found",
        404, logger => logger.LogError("Barber id not found"));
}