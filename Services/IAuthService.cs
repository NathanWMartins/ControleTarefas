using TarefasApi.DTOs;
using TarefasApi.Models;

namespace TarefasApi.Services
{
    public interface IAuthService
    {
        Task<Result<LoginResponseDTO>> Login(LoginRequestDTO dto);
        Task<Result<LoginResponseDTO>> Registrar(LoginRequestDTO dto);
    }
}
