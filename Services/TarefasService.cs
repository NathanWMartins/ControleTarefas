using TarefasApi.Data;
using TarefasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TarefasApi.Services
{
    public class TarefasService
    {
        private readonly AppDbContext _context;

        public TarefasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Tarefa> Tarefas, int TotalTarefas)> GetTarefas(int pagina = 1, int tamanhoPagina = 10)
        {
            var totalTarefas = await _context.Tarefas.CountAsync();
            var tarefas = await _context.Tarefas
                .OrderBy(t => t.Id)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return (tarefas, totalTarefas);
        }

        public async Task<Tarefa> CriarTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task<Tarefa?> GetTarefaPorId(int id)
        {
            return await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tarefa?> ConcluirTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);

            if (tarefa != null)
            {
                tarefa.Concluida = true;
                await _context.SaveChangesAsync();
            }

            return tarefa;
        }

        public async Task<bool> ExcluirTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);

            if (tarefa == null)
                return false;

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}