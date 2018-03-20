using System;
using System.Collections.Generic;
using Eshop.Dashboard.Data.Entities;

namespace Eshop.Dashboard.Data.Seeders
{
    public class UserRolesSeeder
    {
      public static void Seed(EshopDbContext context)
      {
        context.UserRoles.RemoveRange(context.UserRoles);
        context.SaveChanges();

        var userRoles = new List<UserRole>();
        userRoles.Add(new UserRole()
        {
          Id = new Guid("9d8329f1-82f5-4cbb-ba4b-955211457707"),
          Name = "Administrator",
          Code = "Admin"
        });

        userRoles.Add(new UserRole()
        {
          Id = new Guid("4e7f583f-d4f1-4d7a-833e-693fbf99b6e9"),
          Name = "Manager",
          Code = "Manager"
        });

        userRoles.Add(new UserRole()
        {
          Id = new Guid("7446d1fb-a305-440c-b6d9-53769dc1e08a"),
          Name = "Seller",
          Code = "Seller"
        });

        context.UserRoles.AddRange(userRoles);
        context.SaveChanges();
      }
  }
}
