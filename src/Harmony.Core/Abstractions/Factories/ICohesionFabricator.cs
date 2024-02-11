namespace Harmony.Core.Abstractions.Factories;

public interface ICohesionFabricator
{
    TOperation CreateOperation<TOperation, TConfiguration>(Action<TConfiguration> setupConfigAction)
        where TOperation : IHarmonyOperation<TConfiguration>
        where TConfiguration : new();
    
    TOperation CreateOperation<TOperation>() where TOperation : IHarmonyOperation;
}