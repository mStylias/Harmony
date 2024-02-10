using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Harmony.Results.DependencyInjection;

public static class Extensions
{
    public static void UseErrorFactory(this IHost app)
    {
        var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
        ErrorFactory.Logger = loggerFactory.CreateLogger("Errors.Known");
    }
}