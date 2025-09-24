using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.DTOs.Entities
{
    public class UserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    public class UserProfileDto
    {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class UpdateProfileDto
    {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? ProfileImageUrl { get; set; }

    }

}