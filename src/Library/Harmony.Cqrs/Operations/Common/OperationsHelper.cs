using Harmony.Cqrs.Abstractions;

namespace Harmony.Cqrs.Operations.Common;

internal static class OperationsHelper
{
    internal static TMetadata GetMetadataInternal<TMetadata>(this IHarmonyOperation operation) where TMetadata : class
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        if (operation is IWithMetadata<TMetadata> metadataProvider)
        {
            return metadataProvider.Metadata;
        }
    
        throw new InvalidOperationException(
            $"This operation has not implemented IWithMetadata of type {typeof(TMetadata).FullName}.");
    }
}