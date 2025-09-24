using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;

namespace BlazorEcommerce.Client.Services
{
       public class ClientAnalyticsRepository : IClientAnalyticsRepository
    {
        private readonly HttpClient _http;

        public ClientAnalyticsRepository(HttpClient http)
        {
            _http = http;
        }

        public async Task LogVisitAsync(VisitLogRequest request)
        {
            Console.WriteLine($"Logging visit for page: {request.PageVisited}");
            await _http.PostAsJsonAsync("api/analytics/visit", request);
        }
    }

}