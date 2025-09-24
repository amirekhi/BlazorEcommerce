using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlazorEcommerce.Data;
using ClassLibrary1.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClassLibrary1.DTOs.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ClassLibrary1.Interfaces;
using System.Security.Claims;


namespace BlazorEcommerce.Controllers
{
    [ApiController]
    [Route("api/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(DataContext context, IAnalyticsService analyticsService)
        {
            _context = context;
            _analyticsService = analyticsService;
        }

        [HttpPost("visit")]
        public async Task<IActionResult> LogVisit([FromBody] VisitLogRequest request)
        {
            string visitorId;

            // If user is authenticated, use their User ID instead of a cookie-based ID
            if (User.Identity?.IsAuthenticated == true)
            {
                visitorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            else
            {
                visitorId = Request.Cookies["VisitorId"];
                if (string.IsNullOrEmpty(visitorId))
                {
                    visitorId = Guid.NewGuid().ToString();
                    Response.Cookies.Append("VisitorId", visitorId, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(30),
                        HttpOnly = false,
                        SameSite = SameSiteMode.Strict
                    });
                }
            }

            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            var referrer = Request.Headers["Referer"].ToString();

            var visit = new VisitLog
            {
                VisitorId = visitorId,
                SessionId = request.SessionId,
                IsUserRegistered = User.Identity?.IsAuthenticated ?? false,
                Timestamp = DateTime.UtcNow,
                IPAddress = ip,
                UserAgent = userAgent,
                Referrer = referrer,
                PageVisited = request.PageVisited,
                DeviceType = request.DeviceType,
                Country = "iran" // Placeholder
            };

            _context.VisitLogs.Add(visit);
            await _context.SaveChangesAsync();

            return Ok();
        }




        [HttpGet("overview")]
        public async Task<ActionResult<OverviewSummaryDto>> GetOverview()
        {
            var summary = new OverviewSummaryDto
            {
                TotalRevenue = await _context.Orders.SumAsync(o => o.TotalPrice),
                TotalOrders = await _context.Orders.CountAsync(),
                TotalCustomers = await _context.Users.CountAsync(),
                TotalProducts = await _context.Products.CountAsync(),
                RecentOrders = await _context.Orders
                    .OrderByDescending(o => o.CreatedAt)
                    .Take(5)
                    .Select(o => new RecentOrderDto
                    {
                        Id = o.Id,
                        CustomerName = o.User.UserName,
                        Total = o.TotalPrice,
                        Date = o.CreatedAt
                    })
                    .ToListAsync()
            };

            return Ok(summary);
        }

        [HttpGet("sales-overview")]
        public async Task<ActionResult<SalesSummaryDto>> GetSalesOverview()
        {
            var summary = await _analyticsService.GetSalesSummaryAsync();
            return Ok(summary);
        }

        [HttpGet("revenue-trend")]
        public async Task<ActionResult<List<RevenueTrendDto>>> GetRevenueTrend(int days = 30)
        {
            var trend = await _analyticsService.GetRevenueTrendAsync(days);
            return Ok(trend);
        }

        [HttpGet("traffic-summary")]
        public async Task<ActionResult<TrafficAnalyticsDto>> GetTrafficSummary()
        {
            var summary = await _analyticsService.GetTrafficSummaryAsync();
            return Ok(summary);
        }


        [HttpGet("conversion-funnel")]
        public async Task<ActionResult<List<FunnelStepDto>>> GetConversionFunnel()
        {
            var result = await _analyticsService.GetConversionFunnelAsync();
            return Ok(result);
        }

        [HttpGet("pagedropoffs")]
        public async Task<ActionResult<List<PageDropOffDto>>> GetPageDropOffs()
        {
            var result = await _analyticsService.GetPageDropOffStatsAsync();
            return Ok(result);
        }


    }

}