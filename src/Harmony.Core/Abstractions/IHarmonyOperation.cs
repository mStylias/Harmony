using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Core.Abstractions;

public interface IHarmonyOperation : IDisposable
{
    public IServiceScope? Scope { get; set; }
}