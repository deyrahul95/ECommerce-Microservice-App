namespace ProductApi.Application.DTOs;

public record ProductDTO(
    Guid Id,
    string Name,
    int Quantity,
    decimal Price,
    DateTime? CreatedAt = null,
    DateTime? UpdatedAt = null
);