using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Interfaces;

namespace BlazorEcommerce.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _repository;

        public AnalyticsService(IAnalyticsRepository repository)
        {
            _repository = repository;
        }

        public Task<SalesSummaryDto> GetSalesSummaryAsync() => _repository.GetSalesSummaryAsync();
        public Task<List<RevenueTrendDto>> GetRevenueTrendAsync(int days = 30) => _repository.GetRevenueTrendAsync(days);

        public Task<TrafficAnalyticsDto> GetTrafficSummaryAsync()
        {
            return _repository.GetTrafficSummaryAsync();
        }

        public async Task<List<FunnelStepDto>> GetConversionFunnelAsync(int days = 30 )
        {
            var result = await _repository.GetConversionFunnelAsync(days);
            return result;
        }
        
        
        public async Task<List<PageDropOffDto>> GetPageDropOffStatsAsync()
        {
            var result = await _repository.GetPageDropOffStatsAsync();
            return result;
        }
    }
}