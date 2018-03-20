using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Dashboard.Data.Entities
{

  public class Product
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [ForeignKey("CategoryId")]
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    [Required]
    [ForeignKey("VendorId")]
    public Guid VendorId { get; set; }
    public Vendor Vendor { get; set; }

    [MaxLength(255)]
    public string ProductNumber { get; set; }

    [MaxLength(255)]
    public string ProductCode { get; set; }

    [MaxLength(1024)]
    public string ProductPermUrl { get; set; }

    [Required]
    [ForeignKey("ProductStateId")]
    public Guid ProductStateId { get; set; }
    public ProductState ProductState { get; set; }

    [Required]
    [MaxLength(2048)]
    public string Description { get; set; }

    public int? StockCount { get; set; }

    [Required]
    public double Price { get; set; }

    public ICollection<ProductProperty> ProductProperties { get; set; } = new List<ProductProperty>();
  }
}
