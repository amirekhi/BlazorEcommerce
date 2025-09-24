using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.DTOs.Entities
{
    // Summary of key sales metrics
    public class SalesSummaryDto
    {
        public decimal TotalSalesToday { get; set; }
        public decimal TotalSalesThisWeek { get; set; }
        public decimal TotalSalesThisMonth { get; set; }
        public decimal TotalSalesAllTime { get; set; }

        public int OrdersCountToday { get; set; }
        public int OrdersCountThisWeek { get; set; }
        public int OrdersCountThisMonth { get; set; }
        public int OrdersCountAllTime { get; set; }

        public decimal AverageOrderValueToday { get; set; }
        public decimal AverageOrderValueThisWeek { get; set; }
        public decimal AverageOrderValueThisMonth { get; set; }
        public decimal AverageOrderValueAllTime { get; set; }

        public decimal ConversionRateToday { get; set; }
        public decimal ConversionRateThisWeek { get; set; }
        public decimal ConversionRateThisMonth { get; set; }
    }

}