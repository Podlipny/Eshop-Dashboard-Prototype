using Eshop.Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Dashboard.Data
{
  public class EshopDbContext : DbContext
  {
    public EshopDbContext(DbContextOptions<EshopDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductProperty> ProductProperties { get; set; }
    public DbSet<Category> Categories { get; set; }
  }
}
