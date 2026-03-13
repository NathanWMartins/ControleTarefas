using TarefasApi.DTOs;

namespace TarefasApi.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDTO?> Login(LoginRequestDTO dto);
        Task<LoginResponseDTO?> Registrar(LoginRequestDTO dto);
    }
}
