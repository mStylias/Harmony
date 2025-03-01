namespace Harmony.Cqrs.Abstractions;

public interface IWithConfiguration<TConfiguration>
{
    TConfiguration Configuration { get; set; }
}