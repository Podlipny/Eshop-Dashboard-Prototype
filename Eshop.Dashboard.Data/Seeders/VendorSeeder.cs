using System;
using System.Collections.Generic;
using System.Linq;
using Eshop.Dashboard.Data.Entities;

namespace Eshop.Dashboard.Data.Seeders
{
    public class VendorSeeder
    {
    public static void Seed(EshopDbContext context)
    {
      context.Vendors.RemoveRange(context.Vendors);
      context.SaveChanges();

      // init seed data
      Random random = new Random();

      var vendors = new List<Vendor>();

      for (int i = 0; i < 250; i++)
      {
        var dic = LoremNET.Lorem.Words(1).ToUpper();
        vendors.Add(new Vendor()
        {
          Id = Guid.NewGuid(),
          Name = LoremNET.Lorem.Words(2),
          Director = LoremNET.Lorem.Words(2),
          Ico = (int)LoremNET.Lorem.Number(123456789, 99999999),
          Dic = dic.Length > 12 ? dic.Substring(0, 11) : dic,
          ContactId = ContactSeeder.GenerateContact(context)
        });
      }

      context.Vendors.AddRange(vendors);
      context.SaveChanges();
    }

  }
}
