using Microsoft.AspNetCore.Mvc;
using TarefasApi.Models;
using TarefasApi.Services;

namespace TarefasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {

        private readonly TarefasService _service;

        public TarefasController(TarefasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tarefas = await _service.GetTarefas();
            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Tarefa tarefa)
        {
            var novaTarefa = await _service.CriarTarefa(tarefa);
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