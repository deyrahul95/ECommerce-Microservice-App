using ECommerce.Shared.Models;
using OrderApi.Application.DTOs;
using OrderApi.Application.Models;

namespace OrderApi.Application.Services.Interfaces;

public interface IOrderService
{
    public Task<ServiceResult<OrderDTO>> CreateOrder(CreateOrderRequest request, CancellationToken token = default);
    public Task<ServiceResult<OrderDTO>> GetOrder(Guid id, CancellationToken token = default);
    public Task<ServiceResult<List<OrderDTO>>> GetAllOrders(CancellationToken token = default);
    public Task<ServiceResult<List<OrderDTO>>> GetClientOrders(Guid clientId, CancellationToken token = default);
    public Task<ServiceResult<OrderDetailsDTO>> GetOrderDetails(Guid orderId, CancellationToken token = default);
}
