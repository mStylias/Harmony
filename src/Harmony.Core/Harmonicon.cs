using System.Diagnostics;
using Harmony.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Core;

public class Harmonicon : IHarmonicon
{
    private readonly IServiceProvider _serviceProvider;

    public Harmonicon(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    /// <inheritdoc/>
    public TOperation SynthesizeOperation<TOperation>() where TOperation : IHarmonyOperation
    {
        Debug.WriteLineIf(typeof(TOperation).IsAssignableTo(typeof(IOperationWithInput<>)), 
            "Warning: You are synthesizing an operation that requires input without " +
            "using the harmonicon SynthesizeOperation overload that accepts input. " +
            "Consider using that instead.");
        
        Debug.WriteLineIf(typeof(TOperation).IsAssignableTo(typeof(IConfigurable<>)), 
            "Warning: You are synthesizing an operation that requires configuration without " +
            "using the harmonicon SynthesizeOperation overload that accepts configuration. " +
            "Consider using that instead.");
        
        var operation = _serviceProvider.GetRequiredService<TOperation>();
        return operation;
    }
    
    /// <inheritdoc/>
    public TOperation SynthesizeOperation<TOperation, TInput>(TInput input) 
        where TOperation : IOperationWithInput<TInput>
    {
        Debug.WriteLineIf(typeof(TOperation).IsAssignableTo(typeof(IConfigurable<>)), 
            "Warning: You are synthesizing an operation that requires configuration without " +
            "using the harmonicon SynthesizeOperation overload that accepts configuration. " +
            "Consider using that instead.");
        
        var operation = _serviceProvider.GetRequiredService<TOperation>();
        
        operation.Input = input;

        return operation;
    }
        
    /// <inheritdoc/>
    public TOperation SynthesizeOperation<TOperation, TConfiguration>(Action<TConfiguration> setupConfigAction) 
        where TOperation : IHarmonyOperation, IConfigurable<TConfiguration>
        where TConfiguration : new()
    {
        Debug.WriteLineIf(typeof(TOperation).IsAssignableTo(typeof(IConfigurable<>)), 
            "Warning: You are synthesizing an operation that requires configuration without " +
            "using the harmonicon SynthesizeOperation overload that accepts configuration. " +
            "Consider using that instead.");
        
        var operation = _serviceProvider.GetRequiredService<TOperation>();

        var config = new TConfiguration();

        setupConfigAction.Invoke(config);
        operation.Configuration = config;

        return operation;
    }
    
    /// <inheritdoc/>
    public TOperation SynthesizeOperation<TOperation, TInput, TConfiguration>(TInput input, Action<TConfiguration> setupConfigAction) 
        where TOperation : IOperationWithInput<TInput>, IConfigurable<TConfiguration>
        where TConfiguration : new()
    {
        var operation = _serviceProvider.GetRequiredService<TOperation>();

        var config = new TConfiguration();

        setupConfigAction.Invoke(config);
        operation.Configuration = config;
        
        operation.Input = input;

        return operation;
    }
}