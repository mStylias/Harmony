using System.ComponentModel.DataAnnotations.Schema;
using Harmony.EntityFrameworkCore.Localization.Abstractions;

namespace DataAccessDemo.Entities;

[Table("products_localization")]
public class ProductLocalization : ILocalizableEntityTranslation<int>
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("locale")]
    public string Locale { get; set; } = string.Empty;
    
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [Column("description")]
    public string Description { get; set; } = string.Empty;
    
    [Column("product_id")]
    [ForeignKey("Product")]
    public int LocalizableEntityId { get; set; }

    public Product Product { get; set; } = null!;
}