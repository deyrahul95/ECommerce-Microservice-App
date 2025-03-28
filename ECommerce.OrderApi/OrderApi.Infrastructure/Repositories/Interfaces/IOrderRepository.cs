using ECommerce.Shared.Repositories.Interfaces;
using OrderApi.Domain.Entities;

namespace OrderApi.Infrastructure.Repositories.Interfaces;

public interface IOrderRepository : IBaseRepository<Order>
{
    public Task<List<Order>> GetClientOrders(Guid clientId, CancellationToken token = default);
}
