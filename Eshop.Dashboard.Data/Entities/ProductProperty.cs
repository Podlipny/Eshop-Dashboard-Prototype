using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eshop.Dashboard.Data.Entities
{

  public class ProductProperty
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Value { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    public Guid ProductId { get; set; }

  }
}
