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
            var existingProduct = await GetProductByName(request, token);

            if (existingProduct is not null && string.IsNullOrEmpty(existingProduct.Name) == false)
            {
                logger.LogInformation($"Requested Product already exists. Product Id: {existingProduct.Id}, Name: {existingProduct.Name}.");
                return ProductResults<ProductDTO>.PRODUCT_NAME_CONFLICT(request.Name);
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
                logger.LogWarning($"Received null response from product repository. Product Id: {newProduct?.Id.ToString() ?? "N/A"}");
                return ProductResults<ProductDTO>.INTERNAL_SERVICE_FAILURE;
            }

            logger.LogInformation($"Product created successfully. Id: {newProduct.Id}, Name: {newProduct.Name}");
            return ProductResults<ProductDTO>.PRODUCT_CREATED(newProduct.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to create Product.", ex);
            return ProductResults<ProductDTO>.INTERNAL_SERVICE_FAILURE;
        }
    }

    public async Task<ServiceResult<List<ProductDTO>>> GetAllProducts(CancellationToken token = default)
    {
        try
        {
            var products = await productRepository.GetAllAsync(token);

            return ProductResults<List<ProductDTO>>.PRODUCT_FETCHED(products.ToDtoList());
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to fetched Products.", ex);
            return ProductResults<List<ProductDTO>>.INTERNAL_SERVICE_FAILURE;
        }
    }

    public async Task<ServiceResult<ProductDTO>> GetProduct(Guid Id, CancellationToken token = default)
    {
        try
        {
            var product = await productRepository.FindByIdAsync(Id, token);

            if (product is null)
            {
                return ProductResults<ProductDTO>.PRODUCT_NOT_FOUND(Id);
            }

            return ProductResults<ProductDTO>.PRODUCT_FETCHED(product.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to fetched Product. Id: {Id}", ex);
            return ProductResults<ProductDTO>.INTERNAL_SERVICE_FAILURE;
        }
    }

    private async Task<Product?> GetProductByName(CreateProductRequest request, CancellationToken token)
    {
        return await productRepository.GetByAsync(p => p.Name.Equals(
            request.Name,
            StringComparison.OrdinalIgnoreCase), token);
    }
}
