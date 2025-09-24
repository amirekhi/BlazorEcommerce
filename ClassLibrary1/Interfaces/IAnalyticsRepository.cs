using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;

namespace ClassLibrary1.Interfaces
{
    public interface IAnalyticsRepository
    {
        Task<SalesSummaryDto> GetSalesSummaryAsync();
        Task<List<RevenueTrendDto>> GetRevenueTrendAsync(int days = 30);

        Task<TrafficAnalyticsDto> GetTrafficSummaryAsync();
        Task<List<FunnelStepDto>> GetConversionFunnelAsync(int days = 30);
        Task<List<PageDropOffDto>> GetPageDropOffStatsAsync();

    }

}