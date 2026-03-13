using Microsoft.AspNetCore.Mvc;
using TarefasApi.DTOs;
using TarefasApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace TarefasApi.Controllers
{
    [Authorize]
    public class TarefasController : ApiControllerBase
    {
        private readonly ITarefasService _service;

        public TarefasController(ITarefasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            if (pagina < 1) pagina = 1;
            if (tamanhoPagina < 1) tamanhoPagina = 10;

            var result = await _service.GetTarefas(pagina, tamanhoPagina);
            return HandleResult(result);
        }

        [HttpGet("excluidas")]
        public async Task<IActionResult> GetExcluidas([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            if (pagina < 1) pagina = 1;
            if (tamanhoPagina < 1) tamanhoPagina = 10;

            var result = await _service.GetTarefasExcluidas(pagina, tamanhoPagina);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] TarefaRequestDTO tarefaDTO)
        {
            var result = await _service.CriarTarefa(tarefaDTO);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var result = await _service.GetTarefaPorId(id);
            if (!result.IsSuccess) return NotFound(new { result.Message });

            return HandleResult(result);
        }

        [HttpPut("{id}/concluir")]
        public async Task<IActionResult> Concluir(int id)
        {
            var result = await _service.ConcluirTarefa(id);
            if (!result.IsSuccess) return NotFound(new { result.Message });

            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var result = await _service.ExcluirTarefa(id);
            if (!result.IsSuccess) return NotFound(new { result.Message });

            return HandleResult(result);
        }
    }
}