using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;
using System.Net.Http.Json;


namespace BlazorEcommerce.Client.Services
{
    // ClientAnalyticsService.cs
    public class ClientAnalyticsService : IClientAnalyticsService
    {
        private readonly HttpClient _http;

        public ClientAnalyticsService(HttpClient http)
        {
            _http = http;
        }

        public async Task<OverviewSummaryDto?> GetOverviewAsync()
        {
            return await _http.GetFromJsonAsync<OverviewSummaryDto>("api/analytics/overview");
        }

        public async Task<SalesSummaryDto?> GetSalesOverviewAsync()
        {
            return await _http.GetFromJsonAsync<SalesSummaryDto>("api/analytics/sales-overview");
        }
        public async Task<List<RevenueTrendDto>> GetRevenueTrendAsync(int days = 30)
        {
            return await _http.GetFromJsonAsync<List<RevenueTrendDto>>($"api/analytics/revenue-trend?days={days}")
                   ?? new List<RevenueTrendDto>();
        }

        public async Task<TrafficAnalyticsDto?> GetTrafficSummaryAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<TrafficAnalyticsDto>("api/analytics/traffic-summary");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching traffic summary: {ex.Message}");
                return null;
            }
        }


        public async Task<List<FunnelStepDto>> GetConversionFunnelAsync()
        {
            return await _http.GetFromJsonAsync<List<FunnelStepDto>>("api/analytics/conversion-funnel");
        }
        public async Task<List<PageDropOffDto>> GetPageDropOffStatsAsync()
        {
            return await _http.GetFromJsonAsync<List<PageDropOffDto>>("api/analytics/pagedropoffs");
        }
            
        }

}