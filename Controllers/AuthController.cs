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

        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] LoginRequestDTO dto)
        {
            var result = await _authService.Registrar(dto);
            return HandleResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            var result = await _authService.Login(dto);
            return HandleResult(result);
        }
    }
}
