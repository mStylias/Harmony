namespace Harmony.EntityFrameworkCore.Abstractions.Localization;

public interface ILocalizableEntity<TEntityId, TLocalizationEntity> : IEntity<TEntityId>
    where TLocalizationEntity : class, ILocalizationEntity<TEntityId>
{
    ICollection<TLocalizationEntity> Localizations { get; set; }
}