using ProductApi.Application.Models;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs;

public static class ProductMapper
{
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

    public static Product ToEntity(this CreateProductRequest request)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Quantity = request.Quantity,
            Price = request.Price,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
