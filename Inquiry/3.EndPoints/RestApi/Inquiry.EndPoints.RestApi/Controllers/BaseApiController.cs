using Inquiry.Core.Domain.Enums.Response;
using Inquiry.Core.Domain.Models.Response.Entities;
using Inquiry.EndPoints.RestApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Inquiry.EndPoints.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ApiResponseActionFilter]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult OkResponse<T>(T data, string message = "عملیات با موفقیت انجام شد")
        {
            return Ok(ApiResponse<T>.Success(data, message));
        }

        protected IActionResult CreatedResponse<T>(T data, string message = "با موفقیت ایجاد شد")
        {
            return StatusCode(201, ApiResponse<T>.Created(data, message));
        }

        protected IActionResult NoContentResponse()
        {
            return Ok(ApiResponse.NoContent());
        }

        protected IActionResult BadRequestResponse(string message)
        {
            return BadRequest(ApiResponse.Failure(message, ResponseStatus.BadRequest));
        }

        protected IActionResult NotFoundResponse(string message)
        {
            return NotFound(ApiResponse.Failure(message, ResponseStatus.NotFound));
        }

        protected IActionResult PagedResponse<T>(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            return Ok(new PagedResponse<T>(data, pageNumber, pageSize, totalRecords));
        }
    }
}
