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
            OrderedDate: order.OrderedDate,
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

    public static OrderDetailsDTO ToDto(this Order order, ProductDTO product, UserDTO user)
    {
        var totalPrice = order.PurchasedQuantity * product.Price;

        return new OrderDetailsDTO(
            OrderId: order.Id,
            ProductId: product.Id,
            ClientId: user.Id,
            Name: user.Name,
            Email: user.Email,
            Address: user.Address,
            PhoneNumber: user.PhoneNumber,
            ProductName: product.Name,
            PurchasedQuantity: order.PurchasedQuantity,
            UnitPrice: product.Price,
            TotalPrice: totalPrice,
            OrderedDate: order.OrderedDate
        );
    }
}
