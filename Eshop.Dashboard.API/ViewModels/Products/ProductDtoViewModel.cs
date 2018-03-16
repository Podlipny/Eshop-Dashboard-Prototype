using System;

namespace Eshop.Dashboard.API.ViewModels.Products
{
    public class ProductDtoViewModel
    {
      public Guid Id { get; set; }

      public string Name { get; set; }

      public string Description { get; set; }
      
      public string Category { get; set; }

      public double Price { get; set; }
  }
}
