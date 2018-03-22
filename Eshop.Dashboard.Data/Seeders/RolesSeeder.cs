using System;
using System.Collections.Generic;
using Eshop.Dashboard.Data.Entities;

namespace Eshop.Dashboard.Data.Seeders
{
  public class RolesSeeder
  {
    public static void Seed(EshopDbContext context)
    {
      context.Roles.RemoveRange(context.Roles);
      context.UserRoles.RemoveRange(context.UserRoles);
      context.SaveChanges();

      var roles = new List<Role>();
      roles.Add(new Role()
      {
        Id = new Guid("9d8329f1-82f5-4cbb-ba4b-955211457707"),
        Name = "Administrator",
        Code = "Admin"
      });

      roles.Add(new Role()
      {
        Id = new Guid("4e7f583f-d4f1-4d7a-833e-693fbf99b6e9"),
        Name = "Manager",
        Code = "Manager"
      });

      roles.Add(new Role()
      {
        Id = new Guid("7446d1fb-a305-440c-b6d9-53769dc1e08a"),
        Name = "Seller",
        Code = "Seller"
      });

      roles.Add(new Role()
      {
        Id = new Guid("7bf2bcb3-9eb0-4e0d-a88b-9a6937bdee85"),
        Name = "Customer",
        Code = "Customer"
      });

      context.Roles.AddRange(roles);
      context.SaveChanges();
    }

    public static void MapUserRole(EshopDbContext context, Guid userId, Guid roleId)
    {
      var userRole = new UserRole()
      {
        Id = Guid.NewGuid(),
        UserId = userId,
        RoleId = roleId
      };

      context.UserRoles.Add(userRole);
    }

  }
}
