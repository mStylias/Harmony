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
        this._harmonyOperation = serviceProvider.GetRequiredService<TOperation>();
    }

    public OperationBuilder<TOperation> WithInput<TInput>(TInput input)
    {
        var operationWithInput = this._harmonyOperation as IHarmonyOperationWithInput<TInput>;
        const string assertionMessage = "To use the WithInput method, the operation must have " +
            "the input type defined in this method. Use a Command or Query that supports the given Input type.";
        
        Debug.Assert(operationWithInput is not null, assertionMessage);
        
        operationWithInput.Input = input;
#if DEBUG
        this._isInputSet = true;
#endif
        
        return this;
    }

    public OperationBuilder<TOperation> WithConfiguration<TConfiguration>(TConfiguration config) 
        where TConfiguration : class 
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        var operationWithConfiguration = this._harmonyOperation as IConfigurable<TConfiguration>;
        const string assertionMessage = "To use the WithConfiguration method, " +
            "the operation must implement the IConfigurable interface.";
        Debug.Assert(operationWithConfiguration is not null, assertionMessage);

        operationWithConfiguration.Configuration = config;
        
        return this;
    }
    
    public OperationBuilder<TOperation> WithConfiguration<TConfiguration>(Action<TConfiguration> setupConfigAction) 
        where TConfiguration : class, new()
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        var operationWithConfiguration = this._harmonyOperation as IConfigurable<TConfiguration>;
        var assertionMessage = "To use the WithConfiguration method, the operation must " +
                               "implement the IConfigurable interface.";
        
        Debug.Assert(operationWithConfiguration is not null, assertionMessage);

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
        if (inputProperty is not null && this._isInputSet == false)
        {
            var warningMessage = "------------------- Harmony Warning -------------------\n" +
                $"You haven't set the input for {operationType.Name} using the WithInput method. " +
                $"If that is intentional, or if you have set it manually it's safe to ignore this warning.";
            Console.WriteLine(warningMessage);
            Debug.WriteLine(warningMessage);  
        }
#endif
        return this._harmonyOperation;
    }
}