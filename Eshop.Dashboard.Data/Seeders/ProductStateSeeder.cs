using System;
using System.Collections.Generic;
using Eshop.Dashboard.Data.Entities;

namespace Eshop.Dashboard.Data.Seeders
{
  public class ProductStateSeeder
  {
    public static void Seed(EshopDbContext context)
    {
      context.ProductStates.RemoveRange(context.ProductStates);
      context.SaveChanges();

      var productStates = new List<ProductState>();
      productStates.Add(new ProductState()
      {
        Id = new Guid("403d3f4d-3dcf-4516-8720-fc5f9db11b1d"),
        Name = "New",
        Code = 0
      });

      productStates.Add(new ProductState()
      {
        Id = new Guid("c44cd351-69ea-4e20-80be-aecaa6d377dc"),
        Name = "In Catalog",
        Code = 1
      });

      productStates.Add(new ProductState()
      {
        Id = new Guid("a7055d5f-d404-4756-94f9-9bd7271626ad"),
        Name = "Archived",
        Code = 2
      });

      context.ProductStates.AddRange(productStates);
      context.SaveChanges();
    }
  }

}
