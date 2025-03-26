namespace OrderApi.Application.DTOs;

public record OrderDetailsDTO(
    Guid OrderId,
    Guid ProductId,
    Guid ClientId,
    string Email,
    string PhoneNumber,
    string ProductName,
    int PurchasedQuantity,
    decimal UnitPrice,
    decimal TotalPrice,
    DateTime OrderedDate
);
