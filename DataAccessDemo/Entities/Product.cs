using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Harmony.EntityFrameworkCore.Localization.Abstractions;

namespace DataAccessDemo.Entities;

[Table("products")]
public class Product : LocalizableEntity<int, ProductLocalization>
{
    [Column("id")]
    public override int Id { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    public override ICollection<ProductLocalization> Localizations { get; set; } = null!;
}