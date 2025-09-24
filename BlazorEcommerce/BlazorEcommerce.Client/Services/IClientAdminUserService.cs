using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;

namespace BlazorEcommerce.Client.Services
{
    public interface IClientAdminUserService
    {
        Task<List<UserAdminDTO>> GetAllUsersAsync();
        Task<bool> DeactivateUserAsync(string userId);
        Task<bool> ReactivateUserAsync(string userId);
        Task<bool> DeleteUserAsync(string userId);
        Task<UserAdminDTO> GetUserById(string userId);

        Task UpdateUser(string userId , UserAdminDTO user); 
    }
}