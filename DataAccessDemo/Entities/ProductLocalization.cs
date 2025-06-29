using System.ComponentModel.DataAnnotations.Schema;
using Harmony.EntityFrameworkCore.Abstractions;
using Harmony.EntityFrameworkCore.Abstractions.Localization;

namespace DataAccessDemo.Entities;

[Table("products_localization")]
public class ProductLocalization : ILocalizationEntity<int>
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("locale")]
    public string Locale { get; set; } = string.Empty;
    
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("product_id")]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int TargetEntityId
    {
        get => ProductId;
        set => ProductId = value;
    }
}