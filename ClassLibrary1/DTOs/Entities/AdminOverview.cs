using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.DTOs.Entities
{
        public class OverviewSummaryDto
        {
            public decimal TotalRevenue { get; set; }
            public int TotalOrders { get; set; }
            public int TotalCustomers { get; set; }
            public int TotalProducts { get; set; }

            public List<RecentOrderDto> RecentOrders { get; set; } = new();
        }

        public class RecentOrderDto
        {
            public int Id { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public decimal Total { get; set; }
            public DateTime Date { get; set; }
        }
}