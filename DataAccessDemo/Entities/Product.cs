using System.ComponentModel.DataAnnotations;
using Harmony.EntityFrameworkCore;
using Harmony.EntityFrameworkCore.Localization;

namespace DataAccessDemo.Entities;

public class Product : IEntity, ILocalizable
{
    [Key]
    public int Id { get; set; }
    public int? NameLocaleId { get; set; }
    public decimal Price { get; set; }
}