using Eshop.Dashboard.Data.Seeders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eshop.Dashboard.Data
{
  public static class EshopDbContextExtensions
  {
    public static void EnsureSeedDataForContext(this EshopDbContext context)
    {
      // first, clear the database.  This ensures we can always start 
      context.Database.EnsureDeleted();
      context.Database.EnsureCreated();

      RolesSeeder.Seed(context);
      UsersSeeder.Seed(context);

      VendorSeeder.Seed(context);

      CategorySeeder.Seed(context);

      ProductStateSeeder.Seed(context);
      ProductSeeder.Seed(context);

      OrderStateSeeder.Seed(context);
    }
  }

}
