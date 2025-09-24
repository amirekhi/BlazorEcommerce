using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;

namespace ClassLibrary1.Interfaces
{
   public interface IClientOrderRepository
{
    Task<OrderDto> CreateOrderAsync(CreateOrderDto requestDto);
    Task<List<OrderDto>> GetOrdersAsync();
    Task<OrderDto?> GetOrderByIdAsync(int orderId);
}


}