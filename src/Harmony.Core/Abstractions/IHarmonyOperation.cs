namespace Harmony.Core.Abstractions;

public interface IHarmonyOperation<TConfiguration>
{
    TConfiguration? Configuration { get; set; }
}

public interface IHarmonyOperation
{
    
}