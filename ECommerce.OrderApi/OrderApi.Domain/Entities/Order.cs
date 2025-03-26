using System.ComponentModel.DataAnnotations;

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
    public int PurchasedQuantity { get; set; }

    [Required]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
