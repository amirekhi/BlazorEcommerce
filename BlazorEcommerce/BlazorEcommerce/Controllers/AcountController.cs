using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using ClassLibrary1.Entities;
using Microsoft.AspNetCore.Authorization;
using BlazorEcommerce.Extensions;
using ClassLibrary1.DTOs.Entities;


[ApiController]
[Route("api/account")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null) return Unauthorized("username or password");

        var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!passwordValid) return Unauthorized("Invalid username or password");

        // Create JWT token
        var token = GenerateJwtToken(user);

        // Set JWT as HttpOnly, Secure cookie
        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // true for HTTPS only; set false if local dev without HTTPS
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(1)
        });

        return Ok(new { message = "Logged in" });
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userManager.FindByNameAsync(model.Username);
        if (existingUser != null)
            return BadRequest("Username already taken");

        var user = new User
        {
            UserName = model.Username,
            Email = model.Email,
            CreatedAt = DateTime.UtcNow,
            IsAdmin = false,
            ProfileImageUrl = model.ProfileImageUrl
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errors });
        }

        // Optionally log the user in by returning a token
        var token = GenerateJwtToken(user);
        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(1)
        });

        return Ok(new { message = "Registered and logged in" });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await GetCurrentUserAsync();

        if (user == null)
            return NotFound("User not found");

        return Ok(new UserProfileDto
        {
            Username = user.UserName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            IsAdmin = user.IsAdmin,
            ProfileImageUrl = user.ProfileImageUrl
        });
    }

    [HttpGet("whoami")]
    public IActionResult WhoAmI()
    {
       var claims = User.Claims.Select(c => new { c.Type, c.Value });
    return Ok(claims);
    }


    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest model)
    {
        var user = await GetCurrentUserAsync();

        // Check if username is changing and not already taken
        if (user.UserName != model.Username)
        {
            var existingUser = await _userManager.FindByNameAsync(model.Username);
            if (existingUser != null && existingUser.Id != user.Id)
                return BadRequest("Username already taken.");
        }

        user.UserName = model.Username;
        user.Email = model.Email;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errors });
        }

        return Ok(new { message = "Profile updated successfully." });
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountRequest model)
    {
        var user = await GetCurrentUserAsync();

        var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!passwordValid)
            return Unauthorized("Invalid password.");

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errors });
        }

        // Optionally remove JWT cookie
        Response.Cookies.Delete("jwt");

        return Ok(new { message = "Account deleted successfully." });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Append("jwt", "", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(-1)
        });

        return Ok();
    }



    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };

        // Add "Admin" role if user.IsAdmin is true
        if (user.IsAdmin)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public class LoginRequest
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

   

    public class UpdateProfileRequest
    {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
    }

    public class DeleteAccountRequest
    {
        public string Password { get; set; } = default!;
    }

     private async Task<User> GetCurrentUserAsync()
    {
        var userName = User.GetUserName();
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            throw new UnauthorizedAccessException("User not found.");
        return user;
    }
}
