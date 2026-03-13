using TarefasApi.DTOs;

namespace TarefasApi.Services
{
    public interface ITarefasService
    {
        Task<(List<TarefaResponseDTO> Tarefas, int TotalTarefas)> GetTarefas(int pagina = 1, int tamanhoPagina = 10);
        Task<(List<TarefaResponseDTO> Tarefas, int TotalTarefas)> GetTarefasExcluidas(int pagina = 1, int tamanhoPagina = 10);
        Task<TarefaResponseDTO> CriarTarefa(TarefaRequestDTO tarefaDTO);
        Task<TarefaResponseDTO?> GetTarefaPorId(int id);
        Task<TarefaResponseDTO?> ConcluirTarefa(int id);
        Task<bool> ExcluirTarefa(int id);
    }
}
