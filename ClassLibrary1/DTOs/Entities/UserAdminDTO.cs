using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.DTOs.Entities
{
    public class UserAdminDTO
    {
        public string Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string? ProfileImageUrl { get; set; }

    }

}