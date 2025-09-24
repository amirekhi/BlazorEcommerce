using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;

namespace BlazorEcommerce.Client.Services
{
    public interface IClientAnalyticsService
    {
        Task<OverviewSummaryDto?> GetOverviewAsync();
        Task<SalesSummaryDto?> GetSalesOverviewAsync();
        Task<List<RevenueTrendDto>> GetRevenueTrendAsync(int days = 30);

        Task<TrafficAnalyticsDto?> GetTrafficSummaryAsync();
        Task<List<FunnelStepDto>> GetConversionFunnelAsync();
        Task<List<PageDropOffDto>> GetPageDropOffStatsAsync();
    }
}