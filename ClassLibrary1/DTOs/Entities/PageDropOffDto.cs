using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.DTOs.Entities
{
    public class PageDropOffDto
    {
        public string Page { get; set; } = string.Empty;
        public int DropOffCount { get; set; }
        public double DropOffPercentage { get; set; }
    }

}