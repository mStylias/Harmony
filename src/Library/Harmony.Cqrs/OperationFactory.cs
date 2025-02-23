using Harmony.Cqrs.Abstractions;

namespace Harmony.Cqrs;

public class OperationFactory : IOperationFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    public OperationFactory(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider;
    }
    
    /// <inheritdoc/>
    public OperationBuilder<TOperation> CreateBuilder<TOperation>()
        where TOperation : class, IHarmonyOperation
    {
        return new OperationBuilder<TOperation>(this._serviceProvider);
    }
}