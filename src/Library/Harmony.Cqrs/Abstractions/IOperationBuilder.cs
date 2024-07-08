namespace Harmony.Cqrs.Abstractions;

public interface IOperationBuilder<TOperation> where TOperation : class, IHarmonyOperation
{
    OperationBuilder<TOperation> WithInput<TInput>(TInput input);

    OperationBuilder<TOperation> WithConfiguration<TConfiguration>(TConfiguration config) 
        where TConfiguration : class;

    OperationBuilder<TOperation> WithConfiguration<TConfiguration>(Action<TConfiguration> setupConfigAction) 
        where TConfiguration : class, new();

    TOperation Build();
}