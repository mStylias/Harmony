using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Cqrs.Abstractions;

public interface IHarmonyOperation : IDisposable
{
    public IServiceScope? Scope { get; set; }
}