using System.ComponentModel.DataAnnotations;
using OrderApi.Domain.Constants;

namespace OrderApi.Application.Models;

public record CreateOrderRequest(
    [Required] Guid ProductId,
    [Required] Guid ClientId,
    [Required][Range(DBConstraint.PURCHASE_ORDER_MIN_QUANTITY, DBConstraint.PURCHASE_ORDER_MAX_QUANTITY)] int PurchasedQuantity
);
