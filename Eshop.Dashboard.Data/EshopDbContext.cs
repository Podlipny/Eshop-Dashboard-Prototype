using Eshop.Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Dashboard.Data
{
  public class EshopDbContext : DbContext
  {
    public EshopDbContext(DbContextOptions<EshopDbContext> options) : base(options)
    {
    }
    public DbSet<Log> Logs { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Vendor> Vendors { get; set; }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductState> ProductStates { get; set; }
    public DbSet<ProductProperty> ProductProperties { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderState> OrderStates { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

  }
}
