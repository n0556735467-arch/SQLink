using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("mock-login")]
        public IActionResult MockLogin([FromBody] MockLoginDto dto)
        {
            var token = _jwtService.GenerateToken(dto.Email, dto.Role);
            return Ok(new { token });
        }
    }
}