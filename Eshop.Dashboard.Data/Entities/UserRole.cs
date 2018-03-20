using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Dashboard.Data.Entities
{
  public class UserRole
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("UserId")]
    public Guid? UserId { get; set; }
    public User User { get; set; }

    [Required]
    [ForeignKey("RoleId")]
    public Guid? RoleId { get; set; }
    public Role Role { get; set; }
  }
}
