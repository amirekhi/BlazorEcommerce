using System.Net.Http.Json;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Interfaces;

namespace BlazorEcommerce.Client.Services
{
    public class ClientCartRepository : IClientCartRepository
    {
        private readonly HttpClient _http;

        public ClientCartRepository(HttpClient http)
        {
            _http = http;
        }

        public async Task<CartDto?> GetCartAsync()
        {
            return await _http.GetFromJsonAsync<CartDto>("api/cart");
        }

        public async Task<CartDto?> AddItemToCartAsync(AddCartItemDto item)
        {
            var response = await _http.PostAsJsonAsync("api/cart", item);
            return await response.Content.ReadFromJsonAsync<CartDto>();
        }

        public async Task<CartDto?> RemoveItemFromCartAsync(int productId)
        {
            var response = await _http.DeleteAsync($"api/cart/items/{productId}");
            return await response.Content.ReadFromJsonAsync<CartDto>();
        }

        public async Task<CartDto?> UpdateItemQuantityAsync(int productId, int quantity)
        {
            var response = await _http.PutAsJsonAsync($"api/cart/items/{productId}", quantity);
            return await response.Content.ReadFromJsonAsync<CartDto>();
        }

        public async Task<bool> ClearCartAsync()
        {
            var response = await _http.DeleteAsync("api/cart");
            return await response.Content.ReadFromJsonAsync<bool?>() ?? false;
        }
    }
}
