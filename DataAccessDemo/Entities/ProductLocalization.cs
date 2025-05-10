using Harmony.EntityFrameworkCore.Localization;
using Harmony.EntityFrameworkCore.Localization.Abstractions;

namespace DataAccessDemo.Entities;

public class ProductLocalization : ITranslationEntity
{
    public required int ProductId { get; set; }
    public required string Language { get; set; }
    public required string Name { get; set; }
}