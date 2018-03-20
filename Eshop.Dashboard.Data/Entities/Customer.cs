using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Dashboard.Data.Entities
{
  public class Customer
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

    [StringLength(10)]
    public int Ico { get; set; }

    [StringLength(12)]
    public string Dic { get; set; }

    [ForeignKey("ContactId")]
    public Contact Contact { get; set; }
    public Guid? ContactId { get; set; }
  }

}
