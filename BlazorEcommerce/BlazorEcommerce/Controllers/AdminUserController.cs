using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlazorEcommerce.Mappers;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlazorEcommerce.Controllers
{
    [ApiController]
    [Route("api/admin/users")]
    [Authorize(Roles = "Admin")]

    public class AdminUserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AdminUserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserAdminDTO>> GetAllUsers()
        {
            var users = _userManager.Users.Select(u => new UserAdminDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                IsAdmin = u.IsAdmin,
                IsActive = u.IsActive
            });

            return Ok(users);
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");

            user.IsActive = false;
            await _userManager.UpdateAsync(user);

            return Ok("User deactivated.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserAdminDTO>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");
            
            return Ok(user.ToUserAdminDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserAdminDTO updatedUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            user.IsAdmin = updatedUser.IsAdmin;
            user.IsActive = updatedUser.IsActive;
            user.ProfileImageUrl = updatedUser.ProfileImageUrl;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return Ok("User updated successfully.");
        }

        [HttpPut("{id}/reactivate")]
        public async Task<IActionResult> ReactivateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");

            user.IsActive = true;
            await _userManager.UpdateAsync(user);

            return Ok("User reactivated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");

            await _userManager.DeleteAsync(user);
            return Ok("User deleted.");
        }
    }

}