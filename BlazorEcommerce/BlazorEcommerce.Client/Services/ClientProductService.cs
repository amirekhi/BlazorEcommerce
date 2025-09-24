using System.Net.Http.Json;
using BlazorEcommerce.DTOs.Entities;

namespace BlazorEcommerce.Client.Services
{
    public class ClientProductService : IClientProductService
    {
        private readonly HttpClient _http;

        public ClientProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProductDTO>> GetProducts()
        {
            var response = await _http.GetFromJsonAsync<List<ProductDTO>>("api/Product");
            return response ?? new List<ProductDTO>();
        }

        public async Task<ProductDTO?> GetProductById(int id)
        {
            return await _http.GetFromJsonAsync<ProductDTO>($"api/Product/{id}");
        }

        public async Task<ProductDTO> AddProduct(ProductDTO productDTO)
        {
            var response = await _http.PostAsJsonAsync("api/Product", productDTO);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProductDTO>()
                   ?? throw new Exception("Failed to deserialize added product.");
        }

        public async Task<ProductDTO?> EditProduct(int id, ProductDTO newProduct)
        {
            var response = await _http.PutAsJsonAsync($"api/Product/{id}", newProduct);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ProductDTO>();
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var response = await _http.DeleteAsync($"api/Product/{id}");
            return response.IsSuccessStatusCode;
        }


        public async Task<List<ProductDTO>> SearchProductsAsync(string query)
        {
            var response = await _http.GetFromJsonAsync<List<ProductDTO>>($"api/Product/search?query={query}");
            return response ?? new List<ProductDTO>();
        }

    }
}
