using System;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Dashboard.Data.Entities
{
  public class UserRole
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(20)]
    public string Code { get; set; }
  }
}
