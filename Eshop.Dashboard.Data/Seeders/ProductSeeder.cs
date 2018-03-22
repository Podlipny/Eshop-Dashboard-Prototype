using System;
using System.Collections.Generic;
using System.Linq;
using Eshop.Dashboard.Data.Entities;

namespace Eshop.Dashboard.Data.Seeders
{
  public class ProductSeeder
  {
    public static void Seed(EshopDbContext context)
    {
      context.Products.RemoveRange(context.Products);
      context.SaveChanges();

      // init seed data
      Random random = new Random();
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";

      var vendors = context.Vendors.ToList();

      var products = new List<Product>();

      // Category 1 - GUID 25320c5e-f58a-4b1f-b63a-8ee07a840bdf
      for (int i = 0; i < 10; i++)
      {
        products.Add(new Product()
        {
          Id = Guid.NewGuid(),
          Name = LoremNET.Lorem.Words(2),
          Description = LoremNET.Lorem.Words(random.Next(4, 10)),
          Price = random.NextDouble() * (10.2 - 2550.4) + 2550.4,
          CategoryId = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
          ProductStateId = new Guid("c44cd351-69ea-4e20-80be-aecaa6d377dc"),
          VendorId = vendors[random.Next(0, 249)].Id,
          ProductNumber = LoremNET.Lorem.Words(1),
          ProductCode = LoremNET.Lorem.Words(1),
          StockCount = random.Next(0, 100)
        });
      }

      // Category 1.1 - GUID a3749477-f823-4124-aa4a-fc9ad5e79cd6
      for (int i = 0; i < 20; i++)
      {
        products.Add(new Product()
        {
          Id = Guid.NewGuid(),
          Name = LoremNET.Lorem.Words(2),
          Description = LoremNET.Lorem.Words(random.Next(4, 10)),
          Price = random.NextDouble() * (10.2 - 255.4) + 10.2 * -1,
          CategoryId = new Guid("a3749477-f823-4124-aa4a-fc9ad5e79cd6"),
          VendorId = vendors[random.Next(0, 249)].Id,
          ProductStateId = new Guid("c44cd351-69ea-4e20-80be-aecaa6d377dc"),
          ProductNumber = LoremNET.Lorem.Words(1),
          ProductCode = LoremNET.Lorem.Words(1),
          StockCount = random.Next(0, 100)
        });
      }

      // Category 2 - GUID 60188a2b-2784-4fc4-8df8-8919ff838b0b
      for (int i = 0; i < 10000; i++)
      {
        products.Add(new Product()
        {
          Id = Guid.NewGuid(),
          Name = LoremNET.Lorem.Words(2),
          Description = LoremNET.Lorem.Words(random.Next(4, 10)),
          Price = random.NextDouble() * (10.2 - 255.4) + 10.2 * -1,
          CategoryId = new Guid("60188a2b-2784-4fc4-8df8-8919ff838b0b"),
          ProductStateId = new Guid("c44cd351-69ea-4e20-80be-aecaa6d377dc"),
          VendorId = vendors[random.Next(0, 249)].Id,
          ProductNumber = LoremNET.Lorem.Words(1),
          ProductCode = LoremNET.Lorem.Words(1),
          StockCount = random.Next(0, 100)
        });
      }

      context.Products.AddRange(products);
      context.SaveChanges();
    }
  }
}

