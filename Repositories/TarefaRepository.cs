using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Models;
using TarefasApi.Services;

namespace TarefasApi.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserContext _userContext;

        public TarefaRepository(AppDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        private int GetUserIdOrThrow() => _userContext.GetUserId() ?? throw new UnauthorizedAccessException("Usuário não identificado.");

        public async Task<int> GetTotalTarefasAsync()
        {
            var userId = GetUserIdOrThrow();
            return await _context.Tarefas.CountAsync(t => !t.Excluida && t.UsuarioId == userId);
        }

        public async Task<int> GetTotalTarefasExcluidasAsync()
        {
            var userId = GetUserIdOrThrow();
            return await _context.Tarefas.CountAsync(t => t.Excluida && t.UsuarioId == userId);
        }

        public async Task<List<Tarefa>> GetTarefasExcluidasPaginadasAsync(int pagina, int tamanhoPagina)
        {
            var userId = GetUserIdOrThrow();
            return await _context.Tarefas
                .Where(t => t.Excluida && t.UsuarioId == userId)
                .OrderBy(t => t.Id)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public async Task<List<Tarefa>> GetTarefasPaginadasAsync(int pagina, int tamanhoPagina)
        {
            var userId = GetUserIdOrThrow();
            return await _context.Tarefas
                .Where(t => !t.Excluida && t.UsuarioId == userId)
                .OrderBy(t => t.Id)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public async Task<Tarefa?> GetByIdAsync(int id)
        {
            var userId = GetUserIdOrThrow();
            return await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id && !t.Excluida && t.UsuarioId == userId);
        }

        public async Task<Tarefa> AddAsync(Tarefa tarefa)
        {
            tarefa.UsuarioId = GetUserIdOrThrow();
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
