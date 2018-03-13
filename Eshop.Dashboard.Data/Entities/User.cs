using System;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Dashboard.Data.Entities
{
  public class User
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    [MaxLength(512)]
    public string Password { get; set; }

    [Required]
    [MaxLength(50)]
    public string Firstname { get; set; }

    [Required]
    [MaxLength(100)]
    public string Lastname { get; set; }

  }
}
