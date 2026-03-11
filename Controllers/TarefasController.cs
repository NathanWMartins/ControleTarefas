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
        public IActionResult Get()
        {
            return Ok(_service.GetTarefas());
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var novaTarefa = _service.CriarTarefa(tarefa);
            return Ok(novaTarefa);
        }

        [HttpGet("{id}")]
        public IActionResult GetPorId(int id)
        {
            var tarefa = _service.GetTarefaPorId(id);
            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpPut("{id}/concluir")]
        public IActionResult Concluir(int id)
        {
            var tarefa = _service.ConcluirTarefa(id);
            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var sucesso = _service.ExcluirTarefa(id);
            if (!sucesso)
                return NotFound();

            return NoContent();
        }

    }
}