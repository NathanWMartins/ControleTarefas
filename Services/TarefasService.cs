using TarefasApi.Data;
using TarefasApi.Models;
using TarefasApi.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TarefasApi.Services
{
    public class TarefasService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TarefasService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(List<TarefaResponseDTO> Tarefas, int TotalTarefas)> GetTarefas(int pagina = 1, int tamanhoPagina = 10)
        {
            var totalTarefas = await _context.Tarefas.CountAsync();
            var tarefas = await _context.Tarefas
                .OrderBy(t => t.Id)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            var tarefasDTO = _mapper.Map<List<TarefaResponseDTO>>(tarefas);
            return (tarefasDTO, totalTarefas);
        }

        public async Task<TarefaResponseDTO> CriarTarefa(TarefaRequestDTO tarefaDTO)
        {
            var tarefa = _mapper.Map<Tarefa>(tarefaDTO);
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return _mapper.Map<TarefaResponseDTO>(tarefa);
        }

        public async Task<TarefaResponseDTO?> GetTarefaPorId(int id)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
            return tarefa == null ? null : _mapper.Map<TarefaResponseDTO>(tarefa);
        }

        public async Task<TarefaResponseDTO?> ConcluirTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);

            if (tarefa != null)
            {
                tarefa.Concluida = true;
                await _context.SaveChangesAsync();
            }

            return tarefa == null ? null : _mapper.Map<TarefaResponseDTO>(tarefa);
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