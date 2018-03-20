using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Dashboard.Data.Entities
{
  public class Order
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string Code { get; set; }

    [Required]
    [ForeignKey("OrderStateId")]
    public Guid OrderStateId { get; set; }
    public OrderState OrderState { get; set; }

    [Required]
    public DateTime CreatedWhem { get; set; }

    [Required]
    [ForeignKey("CreatedById")]
    public Guid CreatedById { get; set; }
    public User CreatedBy { get; set; }

    public DateTime ProcessingStartedWhem { get; set; }

    [ForeignKey("ProcessedById")]
    public Guid? ProcessedById { get; set; }
    public User ProcessedBy { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
  }
}