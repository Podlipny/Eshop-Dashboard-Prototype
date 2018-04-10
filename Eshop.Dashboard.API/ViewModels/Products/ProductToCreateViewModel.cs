using System;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Dashboard.API.ViewModels.Products
{
  public class ProductToCreateViewModel
  {
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    [Required]
    public Guid VenforId { get; set; }

    [MaxLength(255)]
    public string ProductNumber { get; set; }

    [MaxLength(255)]
    public string ProductCode { get; set; }

    [MaxLength(1024)]
    public string ProductPermUrl { get; set; }

    [Required]
    [MaxLength(2048)]
    public string Description { get; set; }

    public int? StockCount { get; set; }

    [Required]
    public double Price { get; set; }
  }
}
