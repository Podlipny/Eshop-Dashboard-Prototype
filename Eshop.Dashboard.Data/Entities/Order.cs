using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Dashboard.Data.Entities
{
  public class Order
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("OrderStateId")]
    public Guid OrderStateId { get; set; }
    public OrderState OrderState { get; set; }

    [Required]
    [MaxLength(255)]
    public DateTime CreatedWhem { get; set; }

    [Required]
    [ForeignKey("CreatedById")]
    public Guid CreatedById { get; set; }
    public Customer CreatedBy { get; set; }

    [MaxLength(255)]
    public DateTime ProcessingStartedWhem { get; set; }

    [ForeignKey("ProcessedById")]
    public Guid? ProcessedById { get; set; }
    public User ProcessedBy { get; set; }

    [Required]
    [MaxLength(20)]
    public int Code { get; set; }
  }
}