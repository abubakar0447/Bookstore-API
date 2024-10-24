using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookstoreAPI.Models.DTOs;
using BookstoreAPI.Models;
using System.Threading.Tasks;
using System;
using BookstoreAPI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace BookstoreAPI.Controllers
{
    // [ServiceFilter(typeof(DbExceptionFilter))]  // Apply the filter at the controller level
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public CustomerController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // POST: api/User/register
        // [ServiceFilter(typeof(DbExceptionFilter))]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                UserName = registerDTO.Email,
                Email = registerDTO.Email
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Assign Customer role by default
            await _userManager.AddToRoleAsync(user, "Customer");

            // Optionally, assign Admin role based on custom logic (e.g., only one admin)
            var isAdmin = false;  // Custom logic to determine if this user should be an admin
            if (isAdmin)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return Ok(new { message = "User registered successfully!" });
        }

        // POST: api/User/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Email);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            var token = await GenerateJwtToken(user);  // Ensure token is being returned as a string, not a Task or other object
            return Ok(new { Token = token });
        }

        // GET: api/customer/profile
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound(new { Message = "User ID not found in the token" });
            }
            
            if (userId == null)
            {
                return Unauthorized(new { Message = "User ID not found in token" });
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found", userId });
            }

            return Ok(new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(ProfileUpdateDTO profileUpdateDTO)
        {
            // Retrieve the user ID from the token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound(new { Message = "User ID not found in token" });
            }

            // Find the user in the database
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Update the user's profile information
            user.FirstName = profileUpdateDTO.FirstName ?? user.FirstName;
            user.LastName = profileUpdateDTO.LastName ?? user.LastName;

            // Check if the email is being updated
            if (!string.IsNullOrWhiteSpace(profileUpdateDTO.Email) && profileUpdateDTO.Email != user.Email)
            {
                // Check if the new email is already in use
                var existingUser = await _userManager.FindByEmailAsync(profileUpdateDTO.Email);
                if (existingUser != null && existingUser.Id != userId)
                {
                    return BadRequest(new { Message = "Email is already in use" });
                }

                user.Email = profileUpdateDTO.Email;
                user.UserName = profileUpdateDTO.Email; // Update username to match the new email
            }

            // Update the user in the database
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Profile updated successfully" });
        }

        // Generate JWT Token
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);  // Get the user's roles

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Add role claims to the JWT
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
