namespace Harmony.Cqrs.Operations;

public abstract class Command : HarmonyOperation;
public abstract class Command<TOutput> : HarmonyOperation<TOutput>;