using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.DTOs.Entities
{
    public class TrafficAnalyticsDto
    {
        public int TotalVisitsToday { get; set; }
        public int TotalVisitsThisWeek { get; set; }
        public int TotalVisitsThisMonth { get; set; }
        public int TotalVisitsAllTime { get; set; }

        public int UniqueVisitorsToday { get; set; }
        public int UniqueVisitorsThisWeek { get; set; }
        public int UniqueVisitorsThisMonth { get; set; }

        public List<TopReferrerDto> TopReferrers { get; set; }
        public List<TrafficSourceDto> TrafficSources { get; set; }
        public List<VisitedPageDto> MostVisitedPages { get; set; }
        public List<DeviceTypeCountDto> DeviceTypes { get; set; }
        public List<GeoDistributionDto> GeoDistribution { get; set; }

        public double BounceRate { get; set; }
    }

    public class TopReferrerDto
    {
        public string Referrer { get; set; }
        public int Count { get; set; }
    }

    public class TrafficSourceDto
    {
        public string Source { get; set; } // e.g., "Direct", "Referral", "Search"
        public int Count { get; set; }
    }

    public class VisitedPageDto
    {
        public string Page { get; set; }
        public int Count { get; set; }
    }

    public class DeviceTypeCountDto
    {
        public string DeviceType { get; set; } // "Mobile", "Desktop", etc.
        public int Count { get; set; }
    }

    public class GeoDistributionDto
    {
        public string Country { get; set; }
        public int Count { get; set; }
    }

}