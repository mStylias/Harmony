using Harmony.EntityFrameworkCore.Localization;

namespace DataAccessDemo.Entities;

public class ProductLocalization : ILocalizationEntity
{
    public int LocalizationEntryId { get; set; }
    public required string Language { get; set; }
    public required string PropertyName { get; set; }
    public required string Value { get; set; }
}