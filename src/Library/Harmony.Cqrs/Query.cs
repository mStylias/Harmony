using Harmony.Cqrs.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Harmony.Cqrs;

public abstract class Query<TInput, TOutput> : HarmonyOperation<TInput, TOutput> { }
public abstract class Query<TOutput> : HarmonyOperation<TOutput> { }
public abstract class Query : HarmonyOperation { }