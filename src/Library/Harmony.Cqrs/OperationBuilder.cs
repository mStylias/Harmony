using System.Diagnostics;
using Harmony.Cqrs.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Cqrs;

public class OperationBuilder<TOperation>// : IOperationBuilder<TOperation> 
    where TOperation : class, IHarmonyOperation
{
    private readonly TOperation _harmonyOperation;
    
#if DEBUG
    private bool _isInputSet;
#endif
    
    public OperationBuilder(IServiceProvider serviceProvider)
    {
        _harmonyOperation = serviceProvider.GetRequiredService<TOperation>();
    }

    public OperationBuilder<TOperation> WithInput<TInput>(TInput input)
    {
        var operationWithInput = _harmonyOperation as IHarmonyOperationWithInput<TInput>;
        Debug.Assert(operationWithInput is not null, "To use the WithInput method, the operation must have " +
            "the input type defined in this method. Use a Command or Query that supports the given Input type.");
        
        operationWithInput.Input = input;
#if DEBUG
        _isInputSet = true;
#endif
        
        return this;
    }

    public OperationBuilder<TOperation> WithConfiguration<TConfiguration>(TConfiguration config) 
        where TConfiguration : class 
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        var operationWithConfiguration = _harmonyOperation as IConfigurable<TConfiguration>;
        Debug.Assert(operationWithConfiguration is not null, "To use the WithConfiguration method, " +
            "the operation must implement the IConfigurable interface.");

        operationWithConfiguration.Configuration = config;
        
        return this;
    }
    
    public OperationBuilder<TOperation> WithConfiguration<TConfiguration>(Action<TConfiguration> setupConfigAction) 
        where TConfiguration : class, new()
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        var operationWithConfiguration = _harmonyOperation as IConfigurable<TConfiguration>;
        Debug.Assert(operationWithConfiguration is not null, "To use the WithConfiguration method, the operation must " +
            "implement the IConfigurable interface.");

        var config = new TConfiguration();
        setupConfigAction(config);
        operationWithConfiguration.Configuration = config;
        
        return this;
    }
    
    public TOperation Build()
    {
#if DEBUG
        var operationType = typeof(TOperation);
        var inputProperty = operationType.GetProperty(nameof(IHarmonyOperationWithInput<object>.Input));

        // Here we check if the input is set for operations that require it. However, we don't want to disallow
        // passing a null input explicitly. That's why the _isInputSet bool is used.
        if (inputProperty is not null && _isInputSet == false)
        {
            var warningMessage = "------------------- Harmony Warning -------------------\n" +
                $"You haven't set the input for {operationType.Name} using the WithInput method. " +
                $"If that is intentional, or if you have set it manually it's safe to ignore this warning.";
            Console.WriteLine(warningMessage);
            Debug.WriteLine(warningMessage);  
        }
#endif
        return _harmonyOperation;
    }
}