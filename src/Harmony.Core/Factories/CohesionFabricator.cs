using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Core.Factories;

public class CohesionFabricator : ICohesionFabricator
{
    private readonly IServiceProvider _serviceProvider;

    public CohesionFabricator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public T CreateCommand<T>() where T : Command
    {
        return _serviceProvider.GetRequiredService<T>();
    }
    
    public T CreateQuery<T>() where T : Query
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}