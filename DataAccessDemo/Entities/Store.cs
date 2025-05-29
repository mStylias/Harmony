using System.ComponentModel.DataAnnotations.Schema;
using Harmony.EntityFrameworkCore.Abstractions;

namespace DataAccessDemo.Entities;

[Table("stores")]
public class Store : IEntity<int>
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [Column("location")]
    public string Location { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}