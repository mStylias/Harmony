namespace Harmony.Cqrs;

public abstract class Command<TInput, TOutput> : HarmonyOperation<TInput, TOutput> { }
public abstract class Command<TOutput> : HarmonyOperation<TOutput> { }
public abstract class Command : HarmonyOperation { }