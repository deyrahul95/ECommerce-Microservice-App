using System.Net;
using ECommerce.Shared.Models;
using ECommerce.Shared.Services.Interfaces;
using ProductApi.Application.DTOs;
using ProductApi.Application.Models;
using ProductApi.Application.Services.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Repositories.Interfaces;

namespace ProductApi.Application.Services;

public class ProductService(IProductRepository productRepository, ILoggerService logger) : IProductService
{
    public async Task<ServiceResult<ProductDTO>> CreateProduct(CreateProductRequest request, CancellationToken token = default)
    {
        try
        {
            var existingProduct = await productRepository.GetByAsync(p => p.Name.Equals(
                request.Name,
                StringComparison.OrdinalIgnoreCase), token);

            if (existingProduct is not null && string.IsNullOrEmpty(existingProduct.Name) == false)
            {
                return new ServiceResult<ProductDTO>(
                    HttpStatusCode.Conflict,
                    $"Product with name: {request.Name} already exists");
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Quantity = request.Quantity,
                Price = request.Price,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var newProduct = await productRepository.CreateAsync(product, token);

            if (newProduct == null || string.IsNullOrEmpty(newProduct.Name))
            {
                return new ServiceResult<ProductDTO>(
                    HttpStatusCode.InternalServerError,
                        "Internal service failure. Please try again after some time."
                );
            }

            return new ServiceResult<ProductDTO>(
                HttpStatusCode.Created,
                "Product created successfully",
                newProduct.ToDto()
            );
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to create Product.", ex);
            return new ServiceResult<ProductDTO>(
                HttpStatusCode.InternalServerError,
                "An unknown error occurred"
            );
        }
    }
}
