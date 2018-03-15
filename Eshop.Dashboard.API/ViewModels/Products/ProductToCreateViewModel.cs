using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Dashboard.API.ViewModels.Products
{
    public class ProductToCreateViewModel
    {
      public string Name { get; set; }

      public string Description { get; set; }

      public Guid CategoryId { get; set; }

      public double Price { get; set; }
  }
}
