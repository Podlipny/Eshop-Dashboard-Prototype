using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eshop.Dashboard.Data.Entities
{
  public class Category
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [ForeignKey("ParentId")]
    public Category Parent { get; set; }
    public Guid? ParentId { get; set; }

  }
}
