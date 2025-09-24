using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;

namespace BlazorEcommerce.Client.Services
{
    public class ClientAdminUserService : IClientAdminUserService
    {
        private readonly HttpClient _http;

        public ClientAdminUserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UserAdminDTO>> GetAllUsersAsync()
        {
            var response = await _http.GetFromJsonAsync<List<UserAdminDTO>>("api/admin/users");
            return response ?? new List<UserAdminDTO>();
        }

        public async Task<bool> DeactivateUserAsync(string userId)
        {
            var response = await _http.PutAsync($"api/admin/users/{userId}/deactivate", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ReactivateUserAsync(string userId)
        {
            var response = await _http.PutAsync($"api/admin/users/{userId}/reactivate", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var response = await _http.DeleteAsync($"api/admin/users/{userId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<UserAdminDTO> GetUserById(string id) {
            var response = await _http.GetFromJsonAsync<UserAdminDTO>($"api/admin/users/{id}");
            return response;
        }
            

        public async Task UpdateUser(string id, UserAdminDTO user)
        {
            var response = await _http.PutAsJsonAsync($"api/admin/users/{id}", user);
            response.EnsureSuccessStatusCode();
        }

      
    }
}