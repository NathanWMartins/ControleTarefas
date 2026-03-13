using Microsoft.AspNetCore.Mvc;
using TarefasApi.Models;

namespace TarefasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
            {
                return result.Message != null ? Ok(result) : Ok();
            }

            return result.Errors != null ? BadRequest(result) : BadRequest(new { result.Message });
        }

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return result.Errors != null ? BadRequest(result) : BadRequest(new { result.Message });
        }
    }
}
