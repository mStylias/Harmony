namespace Harmony.Cqrs.Abstractions;

public interface IOperationFactory
{
    /// <summary>
    /// Creates a builder for the given operation type. This is the recommended way to configure the operation
    /// </summary>
    /// <typeparam name="TOperation">The type of Command or Query to build</typeparam>
    /// <returns>An OperationBuilder that will create a new instance of the specified operation type</returns>
    public OperationBuilder<TOperation> GetBuilder<TOperation>() where TOperation : class, IHarmonyOperation;
    
    /// <summary>
    /// Creates an operation of type <typeparamref name="TOperation"/> with no input or configuration.
    /// </summary>
    /// <typeparam name="TOperation">The operation type to be created</typeparam>
    [Obsolete("Use the GetBuilder() method instead")]
    TOperation SynthesizeOperation<TOperation>() where TOperation : IHarmonyOperation;
    
    /// <summary>
    /// Creates an operation of type <typeparamref name="TOperation"/> populated with the given <paramref name="input"/>.
    /// </summary>
    /// <param name="input">The input to populate the operation with</param>
    /// <typeparam name="TOperation">The operation type</typeparam>
    /// <typeparam name="TInput">The input type</typeparam>
    /// <returns></returns>
    [Obsolete("Use the GetBuilder() method instead")]
    TOperation SynthesizeOperation<TOperation, TInput>(TInput input) 
        where TOperation : IHarmonyOperationWithInput<TInput>;

    /// <summary>
    /// Creates an operation of type <typeparamref name="TOperation"/> and configures it through the
    /// <paramref name="setupConfigAction"/> delegate.
    /// </summary>
    /// <param name="setupConfigAction">The delegate that sets up the configuration</param>
    /// <typeparam name="TOperation">The harmony operation to create</typeparam>
    /// <typeparam name="TConfiguration">The configuration type of the operation</typeparam>
    [Obsolete("Use the GetBuilder() method instead")]
    TOperation SynthesizeOperation<TOperation, TConfiguration>(Action<TConfiguration> setupConfigAction) 
        where TOperation : IHarmonyOperation, IConfigurable<TConfiguration>
        where TConfiguration : new();

    /// <summary>
    /// Creates an operation of type <typeparamref name="TOperation"/> populated with the given <paramref name="TInput"/>
    /// and configures it through the <paramref name="setupConfigAction"/> delegate.
    /// </summary>
    /// <param name="input">The input data of the operation</param>
    /// <param name="setupConfigAction">The delegate that configures the operation</param>
    /// <typeparam name="TOperation">The operation type</typeparam>
    /// <typeparam name="TInput">The input data type</typeparam>
    /// <typeparam name="TConfiguration">The configuration type</typeparam>
    [Obsolete("Use the GetBuilder() method instead")]
    TOperation SynthesizeOperation<TOperation, TInput, TConfiguration>(TInput input, Action<TConfiguration> setupConfigAction) 
        where TOperation : IHarmonyOperationWithInput<TInput>, IConfigurable<TConfiguration>
        where TConfiguration : new();
}