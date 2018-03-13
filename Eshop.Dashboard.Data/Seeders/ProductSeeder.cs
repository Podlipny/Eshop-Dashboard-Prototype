using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

      var products = new List<Product>();

      // Category 1 - GUID 25320c5e-f58a-4b1f-b63a-8ee07a840bdf
      for (int i = 0; i < 10; i++)
      {
        products.Add(new Product()
        {
          Id = Guid.NewGuid(),
          Name = new string(Enumerable.Repeat(chars, random.Next(10, 100)).Select(s => s[random.Next(s.Length)]).ToArray()),
          Description = new string(Enumerable.Repeat(chars, random.Next(20, 200)).Select(s => s[random.Next(s.Length)]).ToArray()),
          Price = random.NextDouble() * (10.2 - 255.4) + 10.2,
          CategoryId = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf")
        });
      }

      // Category 1.1 - GUID a3749477-f823-4124-aa4a-fc9ad5e79cd6
      for (int i = 0; i < 10; i++)
      {
        products.Add(new Product()
        {
          Id = Guid.NewGuid(),
          Name = new string(Enumerable.Repeat(chars, random.Next(10, 100)).Select(s => s[random.Next(s.Length)]).ToArray()),
          Description = new string(Enumerable.Repeat(chars, random.Next(20, 200)).Select(s => s[random.Next(s.Length)]).ToArray()),
          Price = random.NextDouble() * (10.2 - 255.4) + 10.2,
          CategoryId = new Guid("a3749477-f823-4124-aa4a-fc9ad5e79cd6")
        });
      }

      // Category 2 - GUID 60188a2b-2784-4fc4-8df8-8919ff838b0b
      for (int i = 0; i < 10; i++)
      {
        products.Add(new Product()
        {
          Id = Guid.NewGuid(),
          Name = new string(Enumerable.Repeat(chars, random.Next(10, 100)).Select(s => s[random.Next(s.Length)]).ToArray()),
          Description = new string(Enumerable.Repeat(chars, random.Next(20, 200)).Select(s => s[random.Next(s.Length)]).ToArray()),
          Price = random.NextDouble() * (10.2 - 255.4) + 10.2,
          CategoryId = new Guid("60188a2b-2784-4fc4-8df8-8919ff838b0b")
        });
      }

      context.Products.AddRange(products);
      context.SaveChanges();
    }
  }
}

