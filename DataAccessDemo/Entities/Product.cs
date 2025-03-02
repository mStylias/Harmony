using System.ComponentModel.DataAnnotations;
using Harmony.EntityFrameworkCore;

namespace DataAccessDemo.Entities;

public class Product : IEntity
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}