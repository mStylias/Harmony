using Harmony.EntityFrameworkCore.Abstractions;

namespace Harmony.EntityFrameworkCore.Localization.Abstractions;

public interface ITranslationEntity : IEntity
{
    string Language { get; set; }
}