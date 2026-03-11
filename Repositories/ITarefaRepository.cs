using TarefasApi.Models;

namespace TarefasApi.Repositories
{
    public interface ITarefaRepository
    {
        Task<int> GetTotalTarefasAsync();
        Task<List<Tarefa>> GetTarefasPaginadasAsync(int pagina, int tamanhoPagina);
        Task<Tarefa?> GetByIdAsync(int id);
        Task<Tarefa> AddAsync(Tarefa tarefa);
        Task UpdateAsync(Tarefa tarefa);
        Task DeleteAsync(Tarefa tarefa);
    }
}
