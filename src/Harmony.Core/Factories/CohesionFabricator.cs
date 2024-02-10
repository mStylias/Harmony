using System.Diagnostics;
using Harmony.Core.Abstractions;
using Harmony.Core.Abstractions.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Core.Factories;

public class CohesionFabricator<TConfiguration> : ICohesionFabricator<TConfiguration>
    where TConfiguration : new()
{
    private readonly IServiceProvider _serviceProvider;

    public CohesionFabricator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TOperation CreateOperation<TOperation>(Action<TConfiguration> setupConfigAction)
        where TOperation : IHarmonyOperation<TConfiguration>

    {
        Debug.Assert(typeof(TOperation).IsAssignableTo(typeof(IHarmonyOperation<TConfiguration>)));
        
        var operation = _serviceProvider.GetRequiredService<TOperation>();

        var config = new TConfiguration();

        setupConfigAction.Invoke(config);
        operation.Configuration = config;

        return operation;
    }
}

public class CohesionFabricator : ICohesionFabricator
{
    private readonly IServiceProvider _serviceProvider;

    public CohesionFabricator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TOperation CreateOperation<TOperation>() where TOperation : IHarmonyOperation
    {
        Debug.Assert(typeof(TOperation).IsAssignableTo(typeof(IHarmonyOperation)));
        
        var operation = _serviceProvider.GetRequiredService<TOperation>();
        
        return operation;
    }
}