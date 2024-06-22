// Keeping this namespace to reduce breaking changes
// ReSharper disable once CheckNamespace
namespace Harmony.Cqrs;

public abstract class Query<TInput, TOutput> : HarmonyOperation<TInput, TOutput> { }
public abstract class Query<TOutput> : HarmonyOperation<TOutput> { }
public abstract class Query : HarmonyOperation { }