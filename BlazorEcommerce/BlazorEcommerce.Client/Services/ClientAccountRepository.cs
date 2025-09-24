using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Interfaces;
namespace BlazorEcommerce.Client.Services
{


    public class ClientAccountRepository : IClientAccountRepository
    {
        private readonly HttpClient _http;

        public ClientAccountRepository(HttpClient http)
        {
            _http = http;
        }

        public async Task<UserProfileDto?> GetCurrentUserAsync()
        {
           
            try
            {
                 
                return await _http.GetFromJsonAsync<UserProfileDto>("api/account/me");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateProfileAsync(UpdateProfileDto model)
        {
            var response = await _http.PutAsJsonAsync("api/account/update", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAccountAsync(string password)
        {
            var response = await _http.SendAsync(new HttpRequestMessage(HttpMethod.Delete, "api/account/delete")
            {
                Content = JsonContent.Create(new { Password = password })
            });

            return response.IsSuccessStatusCode;
        }
    }

}