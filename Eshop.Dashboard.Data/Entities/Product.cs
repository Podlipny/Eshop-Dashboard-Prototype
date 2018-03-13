using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
    public Category Category { get; set; }
    public Guid CategoryId { get; set; }

    [Required]
    [MaxLength(2048)]
    public string Description { get; set; }

    [Required]
    public double Price { get; set; }

    public ICollection<ProductProperty> ProductProperties { get; set; }
      = new List<ProductProperty>();
  }
}
