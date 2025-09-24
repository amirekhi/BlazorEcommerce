using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Interfaces;

namespace ClassLibrary1.Services
{


    public class ClientOrderRepository : IClientOrderRepository
    {
        private readonly HttpClient _http;

        public ClientOrderRepository(HttpClient http)
        {
            _http = http;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto requestDto)
        {
            var response = await _http.PostAsJsonAsync("api/orders", requestDto);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Failed to create order");
            }

            var order = await response.Content.ReadFromJsonAsync<OrderDto>();
            return order!;
        }

        public async Task<List<OrderDto>> GetOrdersAsync()
        {
            var orders = await _http.GetFromJsonAsync<List<OrderDto>>("api/orders");
            return orders ?? new List<OrderDto>();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _http.GetFromJsonAsync<OrderDto?>($"api/orders/{orderId}");
            return order;
        }
    }

}