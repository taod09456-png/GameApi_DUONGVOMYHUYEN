using GameApi.DTOs;
using GameApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(
            AuthService authService,
            JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var account = await _authService.Register(dto);

            if (account == null)
            {
                return Conflict(new
                {
                    message = "Username already exists"
                });
            }

            return StatusCode(201, new
            {
                id = account.Id,
                username = account.Username
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var account = await _authService.Login(dto);

            if (account == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                });
            }

            var token = _jwtService.GenerateToken(account);

            return Ok(new
            {
                token = token
            });
        }
    }
}