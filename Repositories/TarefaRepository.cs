using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Models;

namespace TarefasApi.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalTarefasAsync()
        {
            return await _context.Tarefas.CountAsync(t => !t.Excluida);
        }

        public async Task<int> GetTotalTarefasExcluidasAsync()
        {
            return await _context.Tarefas.CountAsync(t => t.Excluida);
        }

        public async Task<List<Tarefa>> GetTarefasExcluidasPaginadasAsync(int pagina, int tamanhoPagina)
        {
            return await _context.Tarefas
                .Where(t => t.Excluida)
                .OrderBy(t => t.Id)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public async Task<List<Tarefa>> GetTarefasPaginadasAsync(int pagina, int tamanhoPagina)
        {
            return await _context.Tarefas
                .Where(t => !t.Excluida)
                .OrderBy(t => t.Id)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public async Task<Tarefa?> GetByIdAsync(int id)
        {
            return await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id && !t.Excluida);
        }

        public async Task<Tarefa> AddAsync(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task UpdateAsync(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tarefa tarefa)
        {
            tarefa.Excluida = true;
            tarefa.DataExclusao = DateTime.UtcNow;
            
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}
