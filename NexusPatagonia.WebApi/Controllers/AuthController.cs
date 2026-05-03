using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Interfaces;

namespace NexusPatagonia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var token = await _authService.AuthenticateAsync(login.Email, login.Password);

            if (token == null)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });

            return Ok(new { token });
        }
    }
}
