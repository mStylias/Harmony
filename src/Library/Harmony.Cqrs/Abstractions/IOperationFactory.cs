namespace Harmony.Cqrs.Abstractions;

public interface IOperationFactory
{
    /// <summary>
    /// Creates a builder for the given operation type. This is the recommended way to configure the operation
    /// </summary>
    /// <typeparam name="TOperation">The type of Command or Query to build</typeparam>
    /// <returns>An OperationBuilder that will create a new instance of the specified operation type</returns>
    public OperationBuilder<TOperation> CreateBuilder<TOperation>() where TOperation : class, IHarmonyOperation;
}