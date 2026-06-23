using FCEService.Domain.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace FCEService.Common
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(ApiResponse<T>.Success(result.Value));
            }

            var topError = result.TopError;

            var statusCode = topError.Type switch
            {
                ErrorKind.Validation  => StatusCodes.Status400BadRequest,
                ErrorKind.NotFound    => StatusCodes.Status404NotFound,
                ErrorKind.Conflict    => StatusCodes.Status409Conflict,
                ErrorKind.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorKind.Forbidden   => StatusCodes.Status403Forbidden,
                _                     => StatusCodes.Status500InternalServerError
            };

            var errorMessages = result.Errors
                .Select(e => new { e.Code, e.Description })
                .ToList();

            var response = ApiResponse<T>.Failure(
                message: topError.Description ?? "An error occurred.",
                errors: errorMessages,
                statusCode: statusCode
            );

            return StatusCode(statusCode, response);
        }
    }
}

