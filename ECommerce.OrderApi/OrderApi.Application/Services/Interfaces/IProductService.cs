using OrderApi.Application.DTOs;

namespace OrderApi.Application.Services.Interfaces;

public interface IProductService
{
    public Task<ProductDTO?> GetProduct(Guid id);
}
