using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eshop.Dashboard.Data.Entities
{
  public class Contact
  {
    [Key]
    public Guid Id { get; set; }

    [MaxLength(12)]
    public int? Telephone { get; set; }

    [Required]
    [MaxLength(512)]
    public string Address1 { get; set; }

    [MaxLength(512)]
    public string Address2 { get; set; }

    [Required]
    [StringLength(6)]
    public int Psc { get; set; }

    [Required]
    [StringLength(255)]
    public string City { get; set; }

    [Required]
    [StringLength(255)]
    public string State { get; set; }

  }

}
