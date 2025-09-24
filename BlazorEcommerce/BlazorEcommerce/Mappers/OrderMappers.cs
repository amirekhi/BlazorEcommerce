using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;

namespace BlazorEcommerce.Mappers
{
    public static class OrderMappers
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto
        {
            Id = order.Id,
            TotalPrice = order.TotalPrice,
            CreatedAt = order.CreatedAt,
            ShippingAddress = order.ShippingAddress,
            PaymentMethod = order.PaymentMethod,
            Items = order.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                ProductImageUrl = i.ProductImageUrl,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };
        }
    }
}