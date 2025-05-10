using DataAccessDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessDemo.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductLocalization> ProductLocalizations { get; set; }
}