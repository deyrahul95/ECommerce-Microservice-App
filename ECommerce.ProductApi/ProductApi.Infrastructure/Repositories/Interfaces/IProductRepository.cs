using ECommerce.Shared.Repositories.Interfaces;
using ProductApi.Domain.Entities;

namespace ProductApi.Infrastructure.Repositories.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    public Task<Product?> GetByName(string name, CancellationToken token = default);
}
