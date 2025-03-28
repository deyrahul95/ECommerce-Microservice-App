using ECommerce.Shared.Models;
using ECommerce.Shared.Services.Interfaces;
using OrderApi.Application.DTOs;
using OrderApi.Application.Services.Interfaces;
using OrderApi.Infrastructure.Repositories.Interfaces;
using Polly.Registry;

namespace OrderApi.Application.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IHttpService httpService,
    ResiliencePipelineProvider<string> pipelineProvider,
    ILoggerService logger) : IOrderService
{
    public async Task<ServiceResult<List<OrderDTO>>> GetClientOrders(Guid clientId, CancellationToken token = default)
    {
        try
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to get client orders. Id: {clientId}", ex);
            return OrderResults<List<OrderDTO>>.INTERNAL_SERVICE_FAILURE;
        }
    }

    public async Task<ServiceResult<OrderDetailsDTO>> GetOrderDetails(Guid orderId, CancellationToken token = default)
    {
        try
        {
            var order = await orderRepository.FindByIdAsync(orderId, token);

            if (order is null)
            {
                return OrderResults<OrderDetailsDTO>.ORDER_NOT_FOUND(orderId);
            }

            // Get retry pipeline
            var retryPipeline = pipelineProvider.GetPipeline("order-retry-pipeline");

            var productDto = await retryPipeline.ExecuteAsync(
                async _token => await httpService.GetProduct(
                    order.ProductId,
                    _token), token);

            if (productDto is null)
            {
                return OrderResults<OrderDetailsDTO>.ORDER_NOT_FOUND(order.ProductId);
            }

            var userDto = await retryPipeline.ExecuteAsync(
                async _token => await httpService.GetUser(
                    order.ClientId,
                    _token), token);

            if (userDto is null)
            {
                return OrderResults<OrderDetailsDTO>.USER_NOT_FOUND(order.ClientId);
            }

            var orderDetails = new OrderDetailsDTO(
                OrderId: orderId,
                ProductId: productDto.Id,
                ClientId: userDto.Id,
                Email: userDto.Email,
                PhoneNumber: userDto.PhoneNumber,
                ProductName: productDto.Name,
                PurchasedQuantity: order.PurchasedQuantity,
                UnitPrice: productDto.Price,
                TotalPrice: order.PurchasedQuantity * productDto.Price,
                OrderedDate: order.OrderedDate
            );

            return OrderResults<OrderDetailsDTO>.ORDER_FETCHED(orderDetails);
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to get order details. Id: {orderId}", ex);
            return OrderResults<OrderDetailsDTO>.INTERNAL_SERVICE_FAILURE;
        }
    }
}
