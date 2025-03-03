namespace Harmony.EntityFrameworkCore.Localization;

public interface ILocalizationEntity : IEntity
{
    int LocalizationEntryId { get; set; }
    string Language { get; set; }
    string PropertyName { get; set; }
    string Value { get; set; }
}