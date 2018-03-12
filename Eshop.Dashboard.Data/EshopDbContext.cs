using Eshop.Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Dashboard.Data
{
  public class EshopDbContext : DbContext
  {
    public EshopDbContext(DbContextOptions<EshopDbContext> options) : base(options)
    {
      Database.Migrate();
    }

    public DbSet<User> Users { get; set; }
  }
}
