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
      context.Database.EnsureCreated();

      UsersSeeder.Seed(context);
      UserRolesSeeder.Seed(context);

      CategorySeeder.Seed(context);
      ProductSeeder.Seed(context);

      OrderStateSeeder.Seed(context);
    }
  }

}
