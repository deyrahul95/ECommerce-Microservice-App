namespace OrderApi.Application.DTOs;

public record OrderDTO(
    Guid Id,
    Guid ProductId,
    Guid ClientId,
    int PurchaseQuantity,
    DateTime OrderedDate,
    DateTime? LastUpdated = null
);
