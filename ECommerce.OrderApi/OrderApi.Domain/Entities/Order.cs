using System.ComponentModel.DataAnnotations;
using OrderApi.Domain.Attributes;
using OrderApi.Domain.Constants;

namespace OrderApi.Domain.Entities;

public class Order
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public Guid ClientId { get; set; }

    [Required]
    [Range(DBConstraint.PURCHASE_ORDER_MIN_QUANTITY, DBConstraint.PURCHASE_ORDER_MAX_QUANTITY)]
    public int PurchasedQuantity { get; set; }

    [Required]
    [TodayDate]
    public DateTime OrderedDate { get; set; } = DateTime.UtcNow;

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
