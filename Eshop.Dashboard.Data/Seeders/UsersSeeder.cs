using Eshop.Dashboard.Data.Entities;
using System;
using System.Collections.Generic;

namespace Eshop.Dashboard.Data.Seeders
{
  public class UsersSeeder
  {
    public static void Seed(EshopDbContext context)
    {
      context.Users.RemoveRange(context.Users);
      context.SaveChanges();

      // init seed data
      Random random = new Random();

      var users = new List<User>();
      var user = new User()
      {
        Id = new Guid("c5e75dd1-1661-424d-8745-e9a002563c27"),
        Username = "admin",
        Password = PasswordHasher.CreateHash("admin123", "AN>1w`YqLNgY})adEzwyQI&qblGW}[5%9MH9de'Ov:R(}U?g/au!>#Mtxk=>@d"),
        Email = "jarmil.prdel@kadibudka.com",
        Firstname = "Jarmil",
        Lastname = "Prdel",
        ContactId = ContactSeeder.GenerateContact(context)
      };
      users.Add(user);

      RolesSeeder.MapUserRole(context, user.Id, new Guid("9d8329f1-82f5-4cbb-ba4b-955211457707")); // administrator
      RolesSeeder.MapUserRole(context, user.Id, new Guid("4e7f583f-d4f1-4d7a-833e-693fbf99b6e9")); // manager
      RolesSeeder.MapUserRole(context, user.Id, new Guid("7446d1fb-a305-440c-b6d9-53769dc1e08a")); // seller


      // other dashboard managers
      for (int i = 0; i < 10; i++)
      {
        user = new User()
        {
          Id = Guid.NewGuid(),
          Username = LoremNET.Lorem.Words(1),
          Email = LoremNET.Lorem.Email(),
          Password = PasswordHasher.CreateHash("Test123", "AN>1w`YqLNgY})adEzwyQI&qblGW}[5%9MH9de'Ov:R(}U?g/au!>#Mtxk=>@d"),
          Firstname = LoremNET.Lorem.Words(1),
          Lastname = LoremNET.Lorem.Words(1),
          ContactId = ContactSeeder.GenerateContact(context)
        };
        users.Add(user);

        RolesSeeder.MapUserRole(context, user.Id, new Guid("4e7f583f-d4f1-4d7a-833e-693fbf99b6e9")); // manager
      }

      // customers
      for (int i = 0; i < 4000; i++)
      {
        var customer = new User()
        {
          Id = Guid.NewGuid(),
          Username = LoremNET.Lorem.Words(1),
          Email = LoremNET.Lorem.Email(),
          Password = PasswordHasher.CreateHash("Test123", "AN>1w`YqLNgY})adEzwyQI&qblGW}[5%9MH9de'Ov:R(}U?g/au!>#Mtxk=>@d"),
          Firstname = LoremNET.Lorem.Words(1),
          Lastname = LoremNET.Lorem.Words(1),
          ContactId = ContactSeeder.GenerateContact(context)
        };
        users.Add(customer);

        RolesSeeder.MapUserRole(context, customer.Id, new Guid("7bf2bcb3-9eb0-4e0d-a88b-9a6937bdee85")); // customer
      }

      context.Users.AddRange(users);
      context.SaveChanges();
    }
  }
}
