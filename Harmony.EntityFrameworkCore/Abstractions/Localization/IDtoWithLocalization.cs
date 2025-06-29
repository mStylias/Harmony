namespace Harmony.EntityFrameworkCore.Abstractions.Localization;

public interface IDtoWithLocalization<TEntityId, TLocalizationDto>
    where TLocalizationDto : class
{
    /// <summary>
    /// Gets or sets the localizations for the entity.
    /// </summary>
    ICollection<TLocalizationDto> LocalizedValues { get; set; }
}