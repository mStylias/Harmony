using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Harmony.EntityFrameworkCore.Localization;
using Harmony.EntityFrameworkCore.Localization.Abstractions;
using Harmony.EntityFrameworkCore.SourceGenerators.Attributes;

namespace DataAccessDemo.Entities;

public class Product : ILocalizableEntity<int>
{
    [Key]
    public int Id { get; set; }
    [LocalizableColumn]
    public required string Name { get; set; }
    public decimal Price { get; set; }

    [NotMapped]
    public int PrimaryKey
    {
        get => Id;
        set => Id = value;
    }
}