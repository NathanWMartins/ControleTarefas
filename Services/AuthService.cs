using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TarefasApi.DTOs;
using TarefasApi.Models;
using TarefasApi.Repositories;
using Microsoft.Extensions.Options;

namespace TarefasApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUsuarioRepository usuarioRepository, IOptions<JwtSettings> jwtSettings)
        {
            _usuarioRepository = usuarioRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<Result<LoginResponseDTO>> Login(LoginRequestDTO dto)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
                return Result.Failure<LoginResponseDTO>("Email ou senha inválidos.", ErrorType.Unauthorized);

            return Result.Success(GerarToken(usuario), "Login realizado com sucesso.");
        }

        public async Task<Result<LoginResponseDTO>> Registrar(LoginRequestDTO dto)
        {
            var usuarioExistente = await _usuarioRepository.GetByEmailAsync(dto.Email);

            if (usuarioExistente != null)
                return Result.Failure<LoginResponseDTO>("Este email já está cadastrado.", ErrorType.Conflict);

            var usuario = new Usuario
            {
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
            };

            await _usuarioRepository.AddAsync(usuario);
            return Result.Success(GerarToken(usuario), "Usuário registrado com sucesso.");
        }

        private LoginResponseDTO GerarToken(Usuario usuario)
        {
            var key = _jwtSettings.Key;
            var issuer = _jwtSettings.Issuer;
            var audience = _jwtSettings.Audience;
            var expiracaoHoras = _jwtSettings.ExpiracaoEmHoras;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiracao = DateTime.UtcNow.AddHours(expiracaoHoras);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiracao,
                signingCredentials: credentials
            );

            return new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = usuario.Email,
                Expiracao = expiracao
            };
        }
    }
}
