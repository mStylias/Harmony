// Keeping this namespace to reduce breaking changes
// ReSharper disable once CheckNamespace

using Harmony.Cqrs.Abstractions;

namespace Harmony.Cqrs;

public abstract class Command<TInput, TOutput> : HarmonyOperation<TInput, TOutput>, IHarmonyOperation;
public abstract class Command<TOutput> : HarmonyOperation<TOutput>, IHarmonyOperation;
public abstract class Command : HarmonyOperation, IHarmonyOperation;