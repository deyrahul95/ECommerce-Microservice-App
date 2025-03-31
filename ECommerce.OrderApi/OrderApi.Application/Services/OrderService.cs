using ECommerce.Shared.Models;
using ECommerce.Shared.Services.Interfaces;
using OrderApi.Application.DTOs;
using OrderApi.Application.Helpers;
using OrderApi.Application.Models;
using OrderApi.Application.Services.Interfaces;
using OrderApi.Infrastructure.Repositories.Interfaces;

namespace OrderApi.Application.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IHttpService httpService,
    RetryHelper retryHelper,
    ILoggerService logger) : IOrderService
{
    public async Task<ServiceResult<OrderDTO>> CreateOrder(CreateOrderRequest request, CancellationToken token = default)
    {
        try
        {
            var productDto = await retryHelper.ExecuteAsync(
                () => httpService.GetProduct(request.ProductId, token), 
                token);

            if (productDto is null)
            {
                return OrderResults<OrderDTO>.PRODUCT_NOT_FOUND(request.ProductId);
            }

            var userDto = await retryHelper.ExecuteAsync(
                () => httpService.GetUser(request.ClientId,token), 
                token);

            if (userDto is null)
            {
                return OrderResults<OrderDTO>.USER_NOT_FOUND(request.ClientId);
            }

            var newOrder = await orderRepository.CreateAsync(request.ToEntity(), token);

            if (newOrder == null || Guid.Empty == newOrder.Id)
            {
                logger.LogWarning($"Received null response from order repository. Order Id: {newOrder?.Id.ToString() ?? "N/A"}");
                return OrderResults<OrderDTO>.INTERNAL_SERVICE_FAILURE;
            }

            logger.LogInformation($"Order created successfully. Id: {newOrder.Id}, Order Date: {newOrder.OrderedDate}");
            return OrderResults<OrderDTO>.ORDER_CREATED(newOrder.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to create order.", ex);
            return OrderResults<OrderDTO>.INTERNAL_SERVICE_FAILURE;
        }
    }

    public async Task<ServiceResult<List<OrderDTO>>> GetAllOrders(CancellationToken token = default)
    {
        try
        {
            var orders = await orderRepository.GetAllAsync(token);

            var sortedOrderDTOs = orders.OrderByDescending(o => o.LastUpdated).ToDtoList();

            return OrderResults<List<OrderDTO>>.ORDER_FETCHED(sortedOrderDTOs);
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to get orders.", ex);
            return OrderResults<List<OrderDTO>>.INTERNAL_SERVICE_FAILURE;
        }
    }

    public async Task<ServiceResult<OrderDTO>> GetOrder(Guid id, CancellationToken token = default)
    {
        try
        {
            var order = await orderRepository.FindByIdAsync(id, token);

            if (order is null)
            {
                return OrderResults<OrderDTO>.ORDER_NOT_FOUND(id);
            }

            return OrderResults<OrderDTO>.ORDER_FETCHED(order.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to get order. Order Id: {id}", ex);
            return OrderResults<OrderDTO>.INTERNAL_SERVICE_FAILURE;
        }
    }

    public async Task<ServiceResult<List<OrderDTO>>> GetClientOrders(Guid clientId, CancellationToken token = default)
    {
        try
        {
            var orders = await orderRepository.GetClientOrders(clientId, token);

            if (orders.Count == 0)
            {
                return OrderResults<List<OrderDTO>>.CLIENT_ORDER_NOT_FOUND(clientId);
            }

            var orderDtoList = orders.ToDtoList();
            return OrderResults<List<OrderDTO>>.ORDER_FETCHED(orderDtoList);
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

            var productDto = await retryHelper.ExecuteAsync(
                () => httpService.GetProduct(order.ProductId, token), 
                token);

            if (productDto is null)
            {
                return OrderResults<OrderDetailsDTO>.PRODUCT_NOT_FOUND(order.ProductId);
            }

            var userDto = await retryHelper.ExecuteAsync(
                () => httpService.GetUser(order.ClientId,token), 
                token);

            if (userDto is null)
            {
                return OrderResults<OrderDetailsDTO>.USER_NOT_FOUND(order.ClientId);
            }

            var orderDetails = order.ToDto(product: productDto, user: userDto);

            return OrderResults<OrderDetailsDTO>.ORDER_FETCHED(orderDetails);
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed to get order details. Id: {orderId}", ex);
            return OrderResults<OrderDetailsDTO>.INTERNAL_SERVICE_FAILURE;
        }
    }
}
