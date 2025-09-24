using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    public class VisitLog
    {
        public int Id { get; set; }
        
        public string SessionId { get; set; } = default!; // Required: generated on client
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? VisitorId { get; set; }  // From cookie or generated client-side
        public bool IsUserRegistered { get; set; }

        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Referrer { get; set; }
        public string? PageVisited { get; set; }
        public string? DeviceType { get; set; } // Mobile, Desktop, Tablet
        public string? Country { get; set; } // Can be null if not resolvable
    }
}