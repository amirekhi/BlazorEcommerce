using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.DTOs.Entities
{
    // Revenue per day for last N days (for trends)
    public class RevenueTrendDto
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public decimal TotalRevenue { get; set; }
    }

}