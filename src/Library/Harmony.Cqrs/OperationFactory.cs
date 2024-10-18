using System.Diagnostics;
using Harmony.Cqrs.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Cqrs;

public class OperationFactory : IOperationFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    public OperationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    /// <inheritdoc/>
    public OperationBuilder<TOperation> GetBuilder<TOperation>() where TOperation : class, IHarmonyOperation
    {
        return new OperationBuilder<TOperation>(_serviceProvider);
    }
    
    /// <inheritdoc/>
    public TOperation SynthesizeOperation<TOperation>() where TOperation : IHarmonyOperation
    {
        Debug.WriteLineIf(typeof(TOperation).IsAssignableTo(typeof(IHarmonyOperationWithInput<>)), 
            "Warning: You are synthesizing an operation that requires input without " +
            "using the SynthesizeOperation overload that accepts input. " +
            "Consider using that instead.");
        
        Debug.WriteLineIf(typeof(TOperation).IsAssignableTo(typeof(IConfigurable<>)), 
            "Warning: You are synthesizing an operation that requires configuration without " +
            "using the SynthesizeOperation overload that accepts configuration. " +
            "Consider using that instead.");
        
        var operation = _serviceProvider.GetRequiredService<TOperation>();
        return operation;
    }
    
    /// <inheritdoc/>
    public TOperation SynthesizeOperation<TOperation, TInput>(TInput input) 
        where TOperation : IHarmonyOperationWithInput<TInput>
    {
        Debug.WriteLineIf(typeof(TOperation).IsAssignableTo(typeof(IConfigurable<>)), 
            "Warning: You are synthesizing an operation that requires configuration without " +
            "using the SynthesizeOperation overload that accepts configuration. " +
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
            "using the SynthesizeOperation overload that accepts configuration. " +
            "Consider using that instead.");
        
        var operation = _serviceProvider.GetRequiredService<TOperation>();

        var config = new TConfiguration();

        setupConfigAction.Invoke(config);
        operation.Configuration = config;

        return operation;
    }
    
    /// <inheritdoc/>
    public TOperation SynthesizeOperation<TOperation, TInput, TConfiguration>(TInput input, Action<TConfiguration> setupConfigAction) 
        where TOperation : IHarmonyOperationWithInput<TInput>, IConfigurable<TConfiguration>
        where TConfiguration : new()
    {
        var operation =_serviceProvider.GetRequiredService<TOperation>();

        var config = new TConfiguration();

        setupConfigAction.Invoke(config);
        operation.Configuration = config;
        
        operation.Input = input;

        return operation;
    }
}