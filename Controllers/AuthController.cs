using Microsoft.AspNetCore.Mvc;
using TarefasApi.DTOs;
using TarefasApi.Services;

namespace TarefasApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>Registra um novo usuário na API.</summary>
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] LoginRequestDTO dto)
        {
            var resultado = await _authService.Registrar(dto);

            if (resultado == null)
                return Conflict(new { mensagem = "Este email já está cadastrado." });

            return Ok(resultado);
        }

        /// <summary>Autentica um usuário e retorna um Token JWT.</summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            var resultado = await _authService.Login(dto);

            if (resultado == null)
                return Unauthorized(new { mensagem = "Email ou senha inválidos." });

            return Ok(resultado);
        }
    }
}
