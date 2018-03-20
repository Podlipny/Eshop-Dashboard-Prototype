using System;
using System.Collections.Generic;
using Eshop.Dashboard.Data.Entities;

namespace Eshop.Dashboard.Data.Seeders
{
    public class OrderStateSeeder
    {
      public static void Seed(EshopDbContext context)
      {
        context.OrderStates.RemoveRange(context.OrderStates);
        context.SaveChanges();

        var orderStates = new List<OrderState>();
        orderStates.Add(new OrderState()
        {
          Id = new Guid("251bffe2-9397-4424-97f9-95059b3d2262"),
          Name = "New",
          Code = 0
        });

        orderStates.Add(new OrderState()
        {
          Id = new Guid("67aaa8f5-bb86-4dd2-bb8c-369a803fddbf"),
          Name = "Processing",
          Code = 1
        });

        orderStates.Add(new OrderState()
        {
          Id = new Guid("60b720b7-6e0d-4139-8de6-ef6f111c690e"),
          Name = "Pending Payment",
          Code = 2
        });

        orderStates.Add(new OrderState()
        {
          Id = new Guid("0b9bf938-8376-49b3-94fb-4e444522d507"),
          Name = "Canceled",
          Code = 3
        });

        orderStates.Add(new OrderState()
        {
          Id = new Guid("c5f9d229-1c83-428a-8e88-9afac4cc0147"),
          Name = "Complete",
          Code = 4
        });

        context.OrderStates.AddRange(orderStates);
        context.SaveChanges();
      }
  }
}
