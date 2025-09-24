using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;

namespace BlazorEcommerce.Mappers
{
    public static class UserMapper
    {
        public static UserAdminDTO ToUserAdminDto(this User user)
        {
           return new UserAdminDTO
           {
               Id = user.Id,
               UserName = user.UserName,
               Email = user.Email,
               CreatedAt = user.CreatedAt,
               IsAdmin = user.IsAdmin,
               IsActive = user.IsActive,
               ProfileImageUrl = user.ProfileImageUrl
           };
        }
    }
}