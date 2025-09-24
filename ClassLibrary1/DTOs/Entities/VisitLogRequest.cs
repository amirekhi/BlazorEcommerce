using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.DTOs.Entities
{
    public class VisitLogRequest
        {
            public string SessionId { get; set; } = default!;
            public string? PageVisited { get; set; }
            public string? DeviceType { get; set; }
         }


}