using TarefasApi.Models;
using TarefasApi.DTOs;
using AutoMapper;
using TarefasApi.Repositories;

namespace TarefasApi.Services
{
    public class TarefasService : ITarefasService
    {
        private readonly ITarefaRepository _repository;
        private readonly IMapper _mapper;

        public TarefasService(ITarefaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<TarefaResponseDTO>>> GetTarefas(int pagina = 1, int tamanhoPagina = 10)
        {
            var totalTarefas = await _repository.GetTotalTarefasAsync();
            var tarefas = await _repository.GetTarefasPaginadasAsync(pagina, tamanhoPagina);

            var tarefasDTO = _mapper.Map<List<TarefaResponseDTO>>(tarefas);
            var pagedResult = new PagedResult<TarefaResponseDTO>(tarefasDTO, totalTarefas, pagina, tamanhoPagina);
            
            return Result.Success(pagedResult);
        }

        public async Task<Result<PagedResult<TarefaResponseDTO>>> GetTarefasExcluidas(int pagina = 1, int tamanhoPagina = 10)
        {
            var totalTarefas = await _repository.GetTotalTarefasExcluidasAsync();
            var tarefas = await _repository.GetTarefasExcluidasPaginadasAsync(pagina, tamanhoPagina);

            var tarefasDTO = _mapper.Map<List<TarefaResponseDTO>>(tarefas);
            var pagedResult = new PagedResult<TarefaResponseDTO>(tarefasDTO, totalTarefas, pagina, tamanhoPagina);

            return Result.Success(pagedResult);
        }

        public async Task<Result<TarefaResponseDTO>> CriarTarefa(TarefaRequestDTO tarefaDTO)
        {
            var tarefa = _mapper.Map<Tarefa>(tarefaDTO);
            await _repository.AddAsync(tarefa);
            var response = _mapper.Map<TarefaResponseDTO>(tarefa);
            return Result.Success(response, "Tarefa criada com sucesso.");
        }

        public async Task<Result<TarefaResponseDTO>> GetTarefaPorId(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            if (tarefa == null)
                return Result.Failure<TarefaResponseDTO>("Tarefa não encontrada.");

            return Result.Success(_mapper.Map<TarefaResponseDTO>(tarefa));
        }

        public async Task<Result<TarefaResponseDTO>> ConcluirTarefa(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);

            if (tarefa == null)
                return Result.Failure<TarefaResponseDTO>("Tarefa não encontrada.");

            tarefa.Concluida = true;
            await _repository.UpdateAsync(tarefa);
            
            return Result.Success(_mapper.Map<TarefaResponseDTO>(tarefa), "Tarefa concluída com sucesso.");
        }

        public async Task<Result> ExcluirTarefa(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);

            if (tarefa == null)
                return Result.Failure("Tarefa não encontrada.");

            await _repository.DeleteAsync(tarefa);

            return Result.Success("Tarefa excluída com sucesso.");
        }
    }
}