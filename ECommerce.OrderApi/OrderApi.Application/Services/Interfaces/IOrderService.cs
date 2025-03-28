using ECommerce.Shared.Models;
using OrderApi.Application.DTOs;

namespace OrderApi.Application.Services.Interfaces;

public interface IOrderService
{
    public Task<ServiceResult<List<OrderDTO>>> GetClientOrders(Guid clientId, CancellationToken token = default);
    public Task<ServiceResult<OrderDetailsDTO>> GetOrderDetails(Guid orderId, CancellationToken token = default);
}
