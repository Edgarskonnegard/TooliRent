using Microsoft.AspNetCore.Mvc;
using TooliRent.Application.DTOs.User;
using TooliRent.Application.Services;

namespace TooliRent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto, CancellationToken ct)
        {
            var token = await _authService.LoginAsync(loginDto.Email, loginDto.Password, ct);

            if (token == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new { token });
        }
    }
}
