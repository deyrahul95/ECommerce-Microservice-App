using ECommerce.Shared.Models;
using ProductApi.Application.DTOs;
using ProductApi.Application.Models;

namespace ProductApi.Application.Services.Interfaces;

public interface IProductService
{
    public Task<ServiceResult<ProductDTO>> CreateProduct(CreateProductRequest request, CancellationToken token = default);
    public Task<ServiceResult<ProductDTO>> GetProduct(Guid Id, CancellationToken token = default);
    public Task<ServiceResult<List<ProductDTO>>> GetAllProducts(CancellationToken token = default);
}
