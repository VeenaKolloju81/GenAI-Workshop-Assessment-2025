using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GenAI_Workshop_Assessment_2025.Services;
using System.Security.Claims;

namespace GenAI_Workshop_Assessment_2025.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = _authService.Login(request.Username, request.Password);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromHeader(Name = "Authorization")] string authHeader)
        {
            try
            {
                var token = authHeader?.Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { message = "Token is required." });

                _authService.Logout(token);
                return Ok(new { message = "Logged out successfully." });
            }
            catch
            {
                return BadRequest(new { message = "Logout failed." });
            }
        }

        [HttpPost("validate")]
        public IActionResult Validate([FromHeader(Name = "Authorization")] string authHeader)
        {
            try
            {
                var token = authHeader?.Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { message = "Token is required." });

                var principal = _authService.ValidateToken(token);
                var username = principal?.Identity?.Name;
                var role = principal?.FindFirst(ClaimTypes.Role)?.Value;
                return Ok(new { username, role });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}