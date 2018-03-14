using System;
using System.Collections.Generic;
using Eshop.Dashboard.Data.Entities;

namespace Eshop.Dashboard.Data.Seeders
{
  public class CategorySeeder
  {
    public static void Seed(EshopDbContext context)
    {
      context.Category.RemoveRange(context.Category);
      context.SaveChanges();

      // init seed data

      var categories = new List<Category>();
      var category1 = new Category()
      {
        Id = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
        Name = "Category 1",
      };
      categories.Add(category1);

      categories.Add(new Category()
      {
        Id = new Guid("a3749477-f823-4124-aa4a-fc9ad5e79cd6"),
        Name = "Category 1.1",
        ParentId = category1.Id
      });

      categories.Add(new Category()
      {
        Id = Guid.NewGuid(),
        Name = "Category 1.2",
        ParentId = category1.Id
      });

      categories.Add(new Category()
      {
        Id = Guid.NewGuid(),
        Name = "Category 1.3",
        ParentId = category1.Id
      });

      var category2 = new Category()
      {
        Id = new Guid("60188a2b-2784-4fc4-8df8-8919ff838b0b"),
        Name = "Category 2"
      };
      categories.Add(category2);

      categories.Add(new Category()
      {
        Id = Guid.NewGuid(),
        Name = "Category 2.1",
        ParentId = category2.Id
      });

      categories.Add(new Category()
      {
        Id = Guid.NewGuid(),
        Name = "Category 2.2",
        ParentId = category2.Id
      });

      categories.Add(new Category()
      {
        Id = Guid.NewGuid(),
        Name = "Category 3",
      });

      categories.Add(new Category()
      {
        Id = Guid.NewGuid(),
        Name = "Category 4",
      });

      context.Category.AddRange(categories);
      context.SaveChanges();
    }
  }
}
