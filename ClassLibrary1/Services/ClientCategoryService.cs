using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;
using ClassLibrary1.Interfaces;

namespace ClassLibrary1.Services
{
   public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CategoryDTO>> GetAllAsync() =>
            await _http.GetFromJsonAsync<List<CategoryDTO>>("api/Category") ?? new();

        public async Task<Category?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Category>($"api/Category/{id}");

       public async Task<Category?> CreateAsync(Category category)
        {
            var response = await _http.PostAsJsonAsync("api/Category", category);

            if (!response.IsSuccessStatusCode)
            {
                // You could log the error or throw an exception here if needed
                return null;
            }

            var createdCategory = await response.Content.ReadFromJsonAsync<Category>();

            return createdCategory;
            }


        public async Task<Category?> UpdateAsync(Category category)
        {
            var response = await _http.PutAsJsonAsync("api/Category", category);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<Category>()
                : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Category/{id}");
            return response.IsSuccessStatusCode;
        }

        Task<CategoryDTO?> ICategoryService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

       

        public Task<CategoryDTO?> CreateAsync(CategoryDTO category)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO?> UpdateAsync(CategoryDTO category, int id)
        {
            throw new NotImplementedException();
        }
    }
}