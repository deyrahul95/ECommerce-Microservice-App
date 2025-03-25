using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs;

public static class ProductMapper
{
    public static Product ToEntity(this ProductDTO dto)
    {
        return new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            Quantity = dto.Quantity,
            Price = dto.Price,
            CreatedAt = dto.CreatedAt ?? DateTime.UtcNow,
            UpdatedAt = dto.UpdatedAt ?? DateTime.UtcNow,
        };
    }

    public static ProductDTO ToDto(this Product product)
    {
        return new ProductDTO(
            Id: product.Id,
            Name: product.Name,
            Quantity: product.Quantity,
            Price: product.Price,
            CreatedAt: product.CreatedAt,
            UpdatedAt: product.UpdatedAt
        );
    }

    public static List<ProductDTO> ToDtoList(this IEnumerable<Product> products)
    {
        if (products == null || products.Any() == false)
        {
            return [];
        }

        var dtoList = products.Select(p => p.ToDto()).ToList();
        return dtoList;
    }
}
