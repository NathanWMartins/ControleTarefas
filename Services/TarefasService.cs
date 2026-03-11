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

        public async Task<List<Tarefa>> GetTarefas()
        {
            return await _context.Tarefas.ToListAsync();
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