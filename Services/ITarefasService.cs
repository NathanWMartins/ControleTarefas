using TarefasApi.DTOs;
using TarefasApi.Models;

namespace TarefasApi.Services
{
    public interface ITarefasService
    {
        Task<Result<PagedResult<TarefaResponseDTO>>> GetTarefas(int pagina = 1, int tamanhoPagina = 10);
        Task<Result<PagedResult<TarefaResponseDTO>>> GetTarefasExcluidas(int pagina = 1, int tamanhoPagina = 10);
        Task<Result<TarefaResponseDTO>> CriarTarefa(TarefaRequestDTO tarefaDTO);
        Task<Result<TarefaResponseDTO>> GetTarefaPorId(int id);
        Task<Result<TarefaResponseDTO>> ConcluirTarefa(int id);
        Task<Result> ExcluirTarefa(int id);
    }
}
