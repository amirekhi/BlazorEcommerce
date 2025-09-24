using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;

namespace BlazorEcommerce.Interfaces
{
    public interface IServerOrderRepository
    {
        Task<Order> CreateOrderAsync(string userId, CreateOrderDto dto);
        Task<List<Order>> GetOrdersForUserAsync(string userId);
        Task<Order?> GetOrderByIdAsync(int orderId, string userId);
    }
}