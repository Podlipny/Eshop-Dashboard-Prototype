using System;

namespace Eshop.Dashboard.Services.Dto
{
  public class ProductDtoViewModel
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string State { get; set; }

    public Guid VendorId { get; set; }

    public string VendorName { get; set; }

    public double Price { get; set; }
  }
}
