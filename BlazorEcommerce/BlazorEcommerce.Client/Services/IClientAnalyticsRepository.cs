using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;

namespace BlazorEcommerce.Client.Services
{
    public interface IClientAnalyticsRepository
    {
        Task LogVisitAsync(VisitLogRequest request);
    }
}