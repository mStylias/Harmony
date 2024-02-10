namespace Harmony.Core.Abstractions.Factories;

public interface ICohesionFabricator<TConfiguration>
    where TConfiguration : new()
{
    TOperation CreateOperation<TOperation>(Action<TConfiguration> setupConfigAction)
        where TOperation : IHarmonyOperation<TConfiguration>;
}

public interface ICohesionFabricator
{
    TOperation CreateOperation<TOperation>() where TOperation : IHarmonyOperation;
}