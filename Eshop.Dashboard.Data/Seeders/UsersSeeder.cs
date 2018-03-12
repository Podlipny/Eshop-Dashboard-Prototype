using Eshop.Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

      var users = new List<User>();
      for (int i = 0; i < 100; i++)
      {
        users.Add(new User()
        {
          Id = Guid.NewGuid(),
          Username = new string(Enumerable.Repeat(chars, random.Next(10, 100)).Select(s => s[random.Next(s.Length)]).ToArray()),
          Password = new string(Enumerable.Repeat(chars, random.Next(10, 200)).Select(s => s[random.Next(s.Length)]).ToArray()),
          FirstName = new string(Enumerable.Repeat(chars, random.Next(8, 40)).Select(s => s[random.Next(s.Length)]).ToArray()),
          LastName = new string(Enumerable.Repeat(chars, random.Next(10, 80)).Select(s => s[random.Next(s.Length)]).ToArray()),
        });
      }

      context.Users.AddRange(users);
      context.SaveChanges();
    }
  }
}
