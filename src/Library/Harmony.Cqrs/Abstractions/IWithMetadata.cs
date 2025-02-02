namespace Harmony.Cqrs.Abstractions;

public interface IWithMetadata<TMetadata>
{
    public TMetadata Metadata { get; set; }
}