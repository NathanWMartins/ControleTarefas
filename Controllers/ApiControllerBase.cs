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

            return MapError(result);
        }

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return MapError(result);
        }

        private IActionResult MapError(Result result)
        {
            return result.ErrorType switch
            {
                ErrorType.NotFound => NotFound(new { result.Message }),
                ErrorType.Unauthorized => Unauthorized(new { result.Message }),
                ErrorType.Conflict => Conflict(new { result.Message }),
                ErrorType.ValidationError => BadRequest(result),
                _ => BadRequest(new { result.Message })
            };
        }
    }
}
