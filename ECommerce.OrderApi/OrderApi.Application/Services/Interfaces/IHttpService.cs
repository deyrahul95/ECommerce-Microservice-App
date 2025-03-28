using OrderApi.Application.DTOs;

namespace OrderApi.Application.Services.Interfaces;

public interface IHttpService
{
    public Task<ProductDTO?> GetProduct(Guid productId, CancellationToken token = default);
    public Task<UserDTO?> GetUser(Guid userId, CancellationToken token = default);
}
