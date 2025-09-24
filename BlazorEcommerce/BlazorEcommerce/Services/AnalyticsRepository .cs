using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorEcommerce.Data;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Services
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly DataContext _context;

        public AnalyticsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<SalesSummaryDto> GetSalesSummaryAsync()
        {
            var today = DateTime.UtcNow.Date;
            var weekStart = today.AddDays(-(int)today.DayOfWeek);
            var monthStart = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);


            var completedOrders = _context.Orders.Where(o =>
                o.Status == "Delivered" || o.Status == "Completed");

            decimal totalSalesAllTime = await completedOrders.SumAsync(o => o.TotalPrice);
            int ordersCountAllTime = await completedOrders.CountAsync();
            decimal avgOrderValueAllTime = ordersCountAllTime == 0 ? 0 : totalSalesAllTime / ordersCountAllTime;

            decimal totalSalesToday = await completedOrders
                .Where(o => o.CreatedAt >= today)
                .SumAsync(o => o.TotalPrice);
            int ordersCountToday = await completedOrders.CountAsync(o => o.CreatedAt >= today);
            decimal avgOrderValueToday = ordersCountToday == 0 ? 0 : totalSalesToday / ordersCountToday;

            decimal totalSalesThisWeek = await completedOrders
                .Where(o => o.CreatedAt >= weekStart)
                .SumAsync(o => o.TotalPrice);
            int ordersCountThisWeek = await completedOrders.CountAsync(o => o.CreatedAt >= weekStart);
            decimal avgOrderValueThisWeek = ordersCountThisWeek == 0 ? 0 : totalSalesThisWeek / ordersCountThisWeek;

            decimal totalSalesThisMonth = await completedOrders
                .Where(o => o.CreatedAt >= monthStart)
                .SumAsync(o => o.TotalPrice);
            int ordersCountThisMonth = await completedOrders.CountAsync(o => o.CreatedAt >= monthStart);
            decimal avgOrderValueThisMonth = ordersCountThisMonth == 0 ? 0 : totalSalesThisMonth / ordersCountThisMonth;

            var visitsToday = await _context.VisitLogs
                .Where(v => v.Timestamp >= today)
                .Select(v => v.VisitorId)
                .Distinct()
                .CountAsync();
            var visitsThisWeek = await _context.VisitLogs
                .Where(v => v.Timestamp >= weekStart)
                .Select(v => v.VisitorId)
                .Distinct()
                .CountAsync();
            var visitsThisMonth = await _context.VisitLogs
                .Where(v => v.Timestamp >= monthStart)
                .Select(v => v.VisitorId)
                .Distinct()
                .CountAsync();

            decimal conversionRateToday = visitsToday == 0 ? 0 : (decimal)ordersCountToday / visitsToday * 100;
            decimal conversionRateThisWeek = visitsThisWeek == 0 ? 0 : (decimal)ordersCountThisWeek / visitsThisWeek * 100;
            decimal conversionRateThisMonth = visitsThisMonth == 0 ? 0 : (decimal)ordersCountThisMonth / visitsThisMonth * 100;

            return new SalesSummaryDto
            {
                TotalSalesToday = totalSalesToday,
                TotalSalesThisWeek = totalSalesThisWeek,
                TotalSalesThisMonth = totalSalesThisMonth,
                TotalSalesAllTime = totalSalesAllTime,

                OrdersCountToday = ordersCountToday,
                OrdersCountThisWeek = ordersCountThisWeek,
                OrdersCountThisMonth = ordersCountThisMonth,
                OrdersCountAllTime = ordersCountAllTime,

                AverageOrderValueToday = avgOrderValueToday,
                AverageOrderValueThisWeek = avgOrderValueThisWeek,
                AverageOrderValueThisMonth = avgOrderValueThisMonth,
                AverageOrderValueAllTime = avgOrderValueAllTime,

                ConversionRateToday = conversionRateToday,
                ConversionRateThisWeek = conversionRateThisWeek,
                ConversionRateThisMonth = conversionRateThisMonth,
            };
        }

        public async Task<List<RevenueTrendDto>> GetRevenueTrendAsync(int days = 30)
        {
            var startDate = DateTime.UtcNow.Date.AddDays(-days + 1);

            return await _context.Orders
                .Where(o => (o.Status == "Delivered" || o.Status == "Completed") && o.CreatedAt >= startDate)
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new RevenueTrendDto
                {
                    Date = g.Key,
                    TotalRevenue = g.Sum(o => o.TotalPrice)
                })
                .OrderBy(r => r.Date)
                .ToListAsync();
        }


        private string InferTrafficSource(string referrer)
        {
            if (string.IsNullOrEmpty(referrer)) return "Direct";
            if (referrer.Contains("google")) return "Search";
            if (referrer.Contains("facebook") || referrer.Contains("twitter")) return "Social";
            return "Referral";
        }

        private async Task<double> CalculateBounceRateAsync()
        {
            var bounces = await _context.VisitLogs
                .GroupBy(v => v.IPAddress)
                .Where(g => g.Count() == 1)
                .CountAsync();

            var total = await _context.VisitLogs.Select(v => v.IPAddress).Distinct().CountAsync();

            return total == 0 ? 0 : (double)bounces / total * 100;
        }

        public async Task<TrafficAnalyticsDto> GetTrafficSummaryAsync()
        {
            var now = DateTime.UtcNow;

            DateTime TruncateToDateUtc(DateTime dt) =>
                new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, DateTimeKind.Utc);

            var today = TruncateToDateUtc(now);
            var weekStart = TruncateToDateUtc(now.AddDays(-(int)now.DayOfWeek));
            var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            var allLogs = _context.VisitLogs.AsNoTracking();

            var uniqueSessions = await allLogs
                .GroupBy(v => v.SessionId)
                .Select(g => g.OrderBy(x => x.Timestamp).FirstOrDefault())
                .ToListAsync();

            var summary = new TrafficAnalyticsDto
            {
                TotalVisitsToday = await allLogs
                    .Where(v => v.Timestamp >= today)
                    .Select(v => v.SessionId)
                    .Distinct()
                    .CountAsync(),

                TotalVisitsThisWeek = await allLogs
                    .Where(v => v.Timestamp >= weekStart)
                    .Select(v => v.SessionId)
                    .Distinct()
                    .CountAsync(),

                TotalVisitsThisMonth = await allLogs
                    .Where(v => v.Timestamp >= monthStart)
                    .Select(v => v.SessionId)
                    .Distinct()
                    .CountAsync(),

                TotalVisitsAllTime = await allLogs
                    .Select(v => v.SessionId)
                    .Distinct()
                    .CountAsync(),

                UniqueVisitorsToday = await allLogs
                    .Where(v => v.Timestamp >= today)
                    .Select(v => v.IPAddress)
                    .Distinct()
                    .CountAsync(),

                UniqueVisitorsThisWeek = await allLogs
                    .Where(v => v.Timestamp >= weekStart)
                    .Select(v => v.IPAddress)
                    .Distinct()
                    .CountAsync(),

                UniqueVisitorsThisMonth = await allLogs
                    .Where(v => v.Timestamp >= monthStart)
                    .Select(v => v.IPAddress)
                    .Distinct()
                    .CountAsync(),

                TopReferrers = uniqueSessions
                    .GroupBy(v => v.Referrer)
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .Select(g => new TopReferrerDto { Referrer = g.Key, Count = g.Count() })
                    .ToList(),

                TrafficSources = uniqueSessions
                    .GroupBy(v => InferTrafficSource(v.Referrer))
                    .Select(g => new TrafficSourceDto { Source = g.Key, Count = g.Count() })
                    .ToList(),

                MostVisitedPages = uniqueSessions
                    .GroupBy(v => v.PageVisited)
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .Select(g => new VisitedPageDto { Page = g.Key, Count = g.Count() })
                    .ToList(),

                DeviceTypes = uniqueSessions
                    .GroupBy(v => v.DeviceType)
                    .Select(g => new DeviceTypeCountDto { DeviceType = g.Key, Count = g.Count() })
                    .ToList(),

                GeoDistribution = uniqueSessions
                    .GroupBy(v => v.Country)
                    .Select(g => new GeoDistributionDto { Country = g.Key, Count = g.Count() })
                    .ToList(),

                BounceRate = await CalculateBounceRateAsync()
            };

            return summary;
        }



        public async Task<List<FunnelStepDto>> GetConversionFunnelAsync(int days = 30)
        {
            var funnelSteps = new List<(string Label, string Route)>
        {
            ("Landing", "/"),
            ("Product", "/products"),
            ("Cart", "/cart"),
            ("Checkout", "/checkout"),
            ("Payment", "/payment"),
            ("Confirmation", "/ordersuccess")
        };

            var funnelCounts = funnelSteps.ToDictionary(fs => fs.Label, fs => 0);

            var visitLogs = await _context.VisitLogs
                .Where(v => v.Timestamp > DateTime.UtcNow.AddDays(-days))
                .GroupBy(v => v.SessionId)
                .ToListAsync();

            foreach (var session in visitLogs)
            {
                var visitedRoutes = session
                    .OrderBy(v => v.Timestamp)
                    .Select(v => v.PageVisited.ToLower())
                    .Distinct()
                    .ToList();

                var seenRoutes = new HashSet<string>();

                foreach (var (label, route) in funnelSteps)
                {
                    if (visitedRoutes.Contains(route.ToLower()))
                    {
                        if (!seenRoutes.Contains(route))
                        {
                            funnelCounts[label]++;
                            seenRoutes.Add(route);
                        }
                    }
                    else
                    {
                        break; // User dropped out
                    }
                }
            }

            return funnelCounts
                .Select(kvp => new FunnelStepDto
                {
                    Step = kvp.Key,
                    Count = kvp.Value
                })
                .ToList();
        }

        public async Task<List<PageDropOffDto>> GetPageDropOffStatsAsync()
        {
            // Step 1: Load all visit logs (or only needed fields if optimized)
            var logs = await _context.VisitLogs
                .AsNoTracking()
                .Select(v => new { v.SessionId, v.PageVisited, v.Timestamp })
                .ToListAsync();

            // Step 2: Group by session and get the last page of each session (in memory)
            var lastPagesPerSession = logs
                .GroupBy(v => v.SessionId)
                .Select(g => g.OrderByDescending(v => v.Timestamp).First())
                .ToList();

            var totalSessions = lastPagesPerSession.Count;

            // Step 3: Count drop-offs per page
            var dropOffCounts = lastPagesPerSession
                .GroupBy(v => v.PageVisited.ToLower())
                .Select(g => new PageDropOffDto
                {
                    Page = g.Key,
                    DropOffCount = g.Count(),
                    DropOffPercentage = Math.Round((double)g.Count() / totalSessions * 100, 2)
                })
                .OrderByDescending(p => p.DropOffPercentage)
                .ToList();

            return dropOffCounts;
        }

    
    }

}