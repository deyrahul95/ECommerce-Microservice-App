using OrderApi.Domain.Entities;

namespace OrderApi.Application.DTOs;

public static class OrderMapper
{
    public static OrderDTO ToDto(this Order order)
    {
        return new OrderDTO(
            Id: order.Id,
            ProductId: order.ProductId,
            ClientId: order.ClientId,
            PurchaseQuantity: order.PurchasedQuantity,
            OrderDate: order.OrderDate,
            LastUpdated: order.LastUpdated
        );
    }

    public static List<OrderDTO> ToDtoList(this IEnumerable<Order> orders)
    {
        if (orders == null || orders.Any() == false)
        {
            return [];
        }

        var dtoList = orders.Select(o => o.ToDto()).ToList();
        return dtoList;
    }
}
