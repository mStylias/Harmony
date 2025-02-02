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
    public OperationBuilder<TOperation> CreateBuilder<TOperation>() where TOperation : class, IHarmonyOperation
    {
        return new OperationBuilder<TOperation>(_serviceProvider);
    }
}