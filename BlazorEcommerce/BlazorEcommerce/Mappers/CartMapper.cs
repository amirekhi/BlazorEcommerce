using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;

namespace BlazorEcommerce.Mappers
{
    public static class CartMapper
    {
        public static CartDto ToCartDto(this Cart cart)
        {
            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = cart.Items.Select(i => new CartItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    ProductName = i.Product?.Name ?? "Unknown",
                    UnitPrice = i.Product?.Price ?? 0,
                    ProductImageUrl = i.Product?.ImageUrl ?? "Unknown"
                }).ToList()
            };
        }
    }
}