namespace Harmony.Core.Abstractions;

public interface IConfigurable<TConfiguration>
{
    TConfiguration Configuration { get; set; }
}