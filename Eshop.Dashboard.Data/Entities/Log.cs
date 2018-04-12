using System;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Dashboard.Data.Entities
{
  public class Log
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string EvenType { get; set; }

    [Required]
    public DateTime CreatedWhen { get; set; }

    [Required]
    public string Message { get; set; }

  }
}
