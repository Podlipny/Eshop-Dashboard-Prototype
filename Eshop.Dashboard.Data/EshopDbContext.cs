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
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductProperty> ProductProperty { get; set; }
    public DbSet<Category> Category { get; set; }
  }
}
