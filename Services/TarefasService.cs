using TarefasApi.Models;
using TarefasApi.DTOs;
using AutoMapper;
using TarefasApi.Repositories;

namespace TarefasApi.Services
{
    public class TarefasService
    {
        private readonly ITarefaRepository _repository;
        private readonly IMapper _mapper;

        public TarefasService(ITarefaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(List<TarefaResponseDTO> Tarefas, int TotalTarefas)> GetTarefas(int pagina = 1, int tamanhoPagina = 10)
        {
            var totalTarefas = await _repository.GetTotalTarefasAsync();
            var tarefas = await _repository.GetTarefasPaginadasAsync(pagina, tamanhoPagina);

            var tarefasDTO = _mapper.Map<List<TarefaResponseDTO>>(tarefas);
            return (tarefasDTO, totalTarefas);
        }

        public async Task<(List<TarefaResponseDTO> Tarefas, int TotalTarefas)> GetTarefasExcluidas(int pagina = 1, int tamanhoPagina = 10)
        {
            var totalTarefas = await _repository.GetTotalTarefasExcluidasAsync();
            var tarefas = await _repository.GetTarefasExcluidasPaginadasAsync(pagina, tamanhoPagina);

            var tarefasDTO = _mapper.Map<List<TarefaResponseDTO>>(tarefas);
            return (tarefasDTO, totalTarefas);
        }

        public async Task<TarefaResponseDTO> CriarTarefa(TarefaRequestDTO tarefaDTO)
        {
            var tarefa = _mapper.Map<Tarefa>(tarefaDTO);
            await _repository.AddAsync(tarefa);
            return _mapper.Map<TarefaResponseDTO>(tarefa);
        }

        public async Task<TarefaResponseDTO?> GetTarefaPorId(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            return tarefa == null ? null : _mapper.Map<TarefaResponseDTO>(tarefa);
        }

        public async Task<TarefaResponseDTO?> ConcluirTarefa(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);

            if (tarefa != null)
            {
                tarefa.Concluida = true;
                await _repository.UpdateAsync(tarefa);
            }

            return tarefa == null ? null : _mapper.Map<TarefaResponseDTO>(tarefa);
        }

        public async Task<bool> ExcluirTarefa(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);

            if (tarefa == null)
                return false;

            await _repository.DeleteAsync(tarefa);

            return true;
        }
    }
}