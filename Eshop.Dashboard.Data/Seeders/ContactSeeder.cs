using System;
using Eshop.Dashboard.Data.Entities;

namespace Eshop.Dashboard.Data.Seeders
{
  public class ContactSeeder
  {
    public static Guid GenerateContact(EshopDbContext context)
    {
      Random random = new Random();

      var contact = new Contact()
      {
        Id = Guid.NewGuid(),
        Telephone = (int)LoremNET.Lorem.Number(600000001, 799999999),
        Address1 = LoremNET.Lorem.Words(random.Next(1, 3)),
        Address2 = LoremNET.Lorem.Words(random.Next(0, 3)),
        Psc = (int)LoremNET.Lorem.Number(10000,49999),
        City = LoremNET.Lorem.Words(1),
        State = LoremNET.Lorem.Words(1)
      };

      context.Contacts.Add(contact);

      return contact.Id;
    }
  }
}
