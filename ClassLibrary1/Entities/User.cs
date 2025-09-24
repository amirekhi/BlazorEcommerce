using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ClassLibrary1.Entities
{

    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; } = false;
        public bool IsActive { get; set; } = true; // default to active

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? ProfileImageUrl { get; set; }
    }

}