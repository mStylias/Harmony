namespace Harmony.Cqrs.Abstractions;

public interface IConfigurable<TConfiguration>
{
    TConfiguration Configuration { get; set; }
}