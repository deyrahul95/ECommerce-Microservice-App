using OrderApi.Application.DTOs;

namespace OrderApi.Application.Services.Interfaces;

public interface IOrderService
{
    public Task<List<OrderDTO>> GetClientOrders(Guid clientId, CancellationToken token = default);
    public Task<OrderDetailsDTO> GetOrderDetails(Guid orderId, CancellationToken token = default);
}
