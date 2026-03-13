using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TarefasApi.DTOs;
using TarefasApi.Models;
using TarefasApi.Repositories;

namespace TarefasApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<Result<LoginResponseDTO>> Login(LoginRequestDTO dto)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
                return Result.Failure<LoginResponseDTO>("Email ou senha inválidos.");

            return Result.Success(GerarToken(usuario), "Login realizado com sucesso.");
        }

        public async Task<Result<LoginResponseDTO>> Registrar(LoginRequestDTO dto)
        {
            var usuarioExistente = await _usuarioRepository.GetByEmailAsync(dto.Email);

            if (usuarioExistente != null)
                return Result.Failure<LoginResponseDTO>("Este email já está cadastrado.");

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
            var key = _configuration["Jwt:Key"]!;
            var issuer = _configuration["Jwt:Issuer"]!;
            var audience = _configuration["Jwt:Audience"]!;
            var expiracaoHoras = int.Parse(_configuration["Jwt:ExpiracaoEmHoras"]!);

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
