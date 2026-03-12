using Microsoft.AspNetCore.Mvc;
using TarefasApi.Models;
using TarefasApi.DTOs;
using TarefasApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace TarefasApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {

        private readonly TarefasService _service;

        public TarefasController(TarefasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            if (pagina < 1) pagina = 1;
            if (tamanhoPagina < 1) tamanhoPagina = 10;

            var (tarefas, totalTarefas) = await _service.GetTarefas(pagina, tamanhoPagina);
            
            return Ok(new 
            {
                TotalTarefas = totalTarefas,
                PaginaAtual = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalPaginas = (int)Math.Ceiling((double)totalTarefas / tamanhoPagina),
                Tarefas = tarefas
            });
        }

        [HttpGet("excluidas")]
        public async Task<IActionResult> GetExcluidas([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            if (pagina < 1) pagina = 1;
            if (tamanhoPagina < 1) tamanhoPagina = 10;

            var (tarefas, totalTarefas) = await _service.GetTarefasExcluidas(pagina, tamanhoPagina);
            
            return Ok(new 
            {
                TotalTarefas = totalTarefas,
                PaginaAtual = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalPaginas = (int)Math.Ceiling((double)totalTarefas / tamanhoPagina),
                Tarefas = tarefas
            });
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] TarefaRequestDTO tarefaDTO)
        {
            var novaTarefa = await _service.CriarTarefa(tarefaDTO);
            return Ok(novaTarefa);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var tarefa = await _service.GetTarefaPorId(id);
            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpPut("{id}/concluir")]
        public async Task<IActionResult> Concluir(int id)
        {
            var tarefa = await _service.ConcluirTarefa(id);
            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var sucesso = await _service.ExcluirTarefa(id);
            if (!sucesso)
                return NotFound();

            return NoContent();
        }

    }
}