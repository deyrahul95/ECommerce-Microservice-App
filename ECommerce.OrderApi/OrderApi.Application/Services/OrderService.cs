using ECommerce.Shared.Services.Interfaces;
using OrderApi.Application.DTOs;
using OrderApi.Application.Services.Interfaces;
using OrderApi.Infrastructure.Repositories.Interfaces;

namespace OrderApi.Application.Services;

public class OrderService(IOrderRepository orderRepository, ILoggerService logger) : IOrderService
{
    public Task<List<OrderDTO>> GetClientOrders(Guid clientId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDetailsDTO> GetOrderDetails(Guid orderId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
