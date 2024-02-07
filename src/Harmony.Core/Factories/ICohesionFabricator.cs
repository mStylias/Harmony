namespace Harmony.Core.Factories;

public interface ICohesionFabricator
{
    T CreateCommand<T>() where T : Command;
    T CreateQuery<T>() where T : Query;
}