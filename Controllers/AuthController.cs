using Microsoft.AspNetCore.Mvc;
using TarefasApi.DTOs;
using TarefasApi.Services;

namespace TarefasApi.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] LoginRequestDTO dto)
        {
            var result = await _authService.Registrar(dto);
            if (!result.IsSuccess && result.Message == "Este email já está cadastrado.")
                return Conflict(new { result.Message });

            return HandleResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            var result = await _authService.Login(dto);
            if (!result.IsSuccess)
                return Unauthorized(new { result.Message });

            return HandleResult(result);
        }
    }
}
