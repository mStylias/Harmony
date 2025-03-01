namespace Harmony.Cqrs.Operations;

public abstract class Query : HarmonyOperation;
public abstract class Query<TOutput> : HarmonyOperation<TOutput>;