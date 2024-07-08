using System.Diagnostics;
using Harmony.Cqrs.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Cqrs;

public class OperationFactory : IOperationFactory
{
    internal static bool UseScopeFactory { get; set; }
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceScopeFactory? _serviceScopeFactory;
    
    public OperationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        if (UseScopeFactory)
        {
            _serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        }
    }
    
    /// <inheritdoc/>
    public OperationBuilder<TOperation> GetBuilder<TOperation>() where TOperation : class, IHarmonyOperation
    {
        return new(_serviceProvider);
    }
    
    /// <inheritdoc/>
    public TOperation SynthesizeOperation<TOperation>() where TOperation : IHarmonyOperation
    {
        Debug.WriteLineIf(typeof(TOperation).IsAssignableTo(typeof(IHarmonyOperationWithInput<>)), 
            "Warning: You are synthesizing an operation that requires input without " +
            "using the harmonicon SynthesizeOperation overload that accepts input. " +
            "Consider using that instead.");
        
        Debug.WriteLineIf(typeof(TOperation).IsAssignableTo(typeof(IConfigurable<>)), 
            "Warning: You are synthesizing an operation that requires configuration without " +
            "using the harmonicon SynthesizeOperation overload that accepts configuration. " +
            "Consider using that instead.");
        
        var operation = CreateOperation<TOperation>();
        return operation;
    }
    
    /// <inheritdoc/>
    public TOperation SynthesizeOperation<TOperation, TInput>(TInput input) 
        where TOperation : IHarmonyOperationWithInput<TInput>
    {
        Debug.WriteLineIf(typeof(TOperation).IsAssignableTo(typeof(IConfigurable<>)), 
            "Warning: You are synthesizing an operation that requires configuration without " +
            "using the harmonicon SynthesizeOperation overload that accepts configuration. " +
            "Consider using that instead.");

        var operation = CreateOperation<TOperation>();
        
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
        
        var operation = CreateOperation<TOperation>();

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
        var operation = CreateOperation<TOperation>();

        var config = new TConfiguration();

        setupConfigAction.Invoke(config);
        operation.Configuration = config;
        
        operation.Input = input;

        return operation;
    }

    private TOperation CreateOperation<TOperation>()
        where TOperation : IHarmonyOperation
    {
        TOperation operation;
        
        if (UseScopeFactory)
        {
            var scope = _serviceScopeFactory!.CreateScope();
            operation = scope.ServiceProvider.GetRequiredService<TOperation>();
            operation.Scope = scope;
        }
        else
        {
            operation = _serviceProvider.GetRequiredService<TOperation>();
        }

        return operation;
    }
}