using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Dashboard.Data.Entities
{
  /// <summary>
  /// Binding entity from orders and products
  /// </summary>
  public class OrderItem
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("OrderId")]
    public Guid? OrderId { get; set; }
    public Order Order { get; set; }

    [Required]
    [ForeignKey("ProductId")]
    public Guid? ProductId { get; set; }
    public Product Product { get; set; }
  }
}
