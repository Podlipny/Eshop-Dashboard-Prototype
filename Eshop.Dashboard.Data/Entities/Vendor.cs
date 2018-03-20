using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Dashboard.Data.Entities
{
  public class Vendor
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(512)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Director { get; set; }

    [Required]
    [StringLength(10)]
    public int Ico { get; set; }

    [StringLength(12)]
    public string Dic { get; set; }

    [ForeignKey("ContactId")]
    public Contact Contact { get; set; }
    public Guid? ContactId { get; set; }
  }
}
