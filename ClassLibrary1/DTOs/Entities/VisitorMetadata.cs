using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.DTOs.Entities
{
    public class VisitorMetadata
    {
        public string Referrer { get; set; }
        public string UserAgent { get; set; }
        public string DeviceType { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public string? PageVisited { get; set; }
    }
}