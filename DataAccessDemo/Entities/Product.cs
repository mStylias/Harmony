using System.ComponentModel.DataAnnotations.Schema;
using Harmony.EntityFrameworkCore.Abstractions.Localization;

namespace DataAccessDemo.Entities;

[Table("products")]
public class Product : ILocalizableEntity<int, ProductLocalization> // IEntity<int>
{
    [Column("product_id")]
    public int Id { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    public ICollection<ProductLocalization> Localizations { get; set; } = new List<ProductLocalization>();
    
    [Column("store_id")]
    [ForeignKey("Store")]
    public int StoreId { get; set; }
    public Store? Store { get; set; }
}
