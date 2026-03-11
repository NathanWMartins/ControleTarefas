using TarefasApi.Data;
using TarefasApi.Models;

namespace TarefasApi.Services
{
    public class TarefasService
    {
        private readonly AppDbContext _context;

        public TarefasService(AppDbContext context)
        {
            _context = context;
        }

        public List<Tarefa> GetTarefas()
        {
            return _context.Tarefas.ToList();
        }

        public Tarefa CriarTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return tarefa;
        }

        public Tarefa? GetTarefaPorId(int id)
        {
            return _context.Tarefas.FirstOrDefault(t => t.Id == id);
        }

        public Tarefa? ConcluirTarefa(int id)
        {
            var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa != null)
            {
                tarefa.Concluida = true;
                _context.SaveChanges();
            }
            return tarefa;
        }

        public bool ExcluirTarefa(int id)
        {
            var tarefaExistente = _context.Tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefaExistente == null) return false;

            _context.Tarefas.Remove(tarefaExistente);
            _context.SaveChanges();
            return true;
        }
    }
}