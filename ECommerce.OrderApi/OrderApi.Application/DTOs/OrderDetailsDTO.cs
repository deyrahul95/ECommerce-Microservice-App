namespace OrderApi.Application.DTOs;

public record OrderDetailsDTO(
    Guid OrderId,
    Guid ProductId,
    Guid ClientId,
    string Name,
    string Email,
    string Address,
    string PhoneNumber,
    string ProductName,
    int PurchasedQuantity,
    decimal UnitPrice,
    decimal TotalPrice,
    DateTime OrderedDate
);
