using System;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Dashboard.Data.Entities
{
  public class ProductState
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(20)]
    public int Code { get; set; }
  }
}
