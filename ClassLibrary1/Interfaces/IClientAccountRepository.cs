using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;

namespace ClassLibrary1.Interfaces
{
    public interface IClientAccountRepository
    {
        Task<UserProfileDto?> GetCurrentUserAsync();
        Task<bool> UpdateProfileAsync(UpdateProfileDto model);
        Task<bool> DeleteAccountAsync(string password);
    }

}