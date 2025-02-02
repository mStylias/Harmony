// Keeping this namespace to reduce breaking changes
// ReSharper disable once CheckNamespace

using Harmony.Cqrs.Abstractions;

namespace Harmony.Cqrs;

public abstract class Query<TInput, TOutput> : HarmonyOperation<TInput, TOutput>, IHarmonyOperation;
public abstract class Query<TOutput> : HarmonyOperation<TOutput>, IHarmonyOperation;
public abstract class Query : HarmonyOperation, IHarmonyOperation;