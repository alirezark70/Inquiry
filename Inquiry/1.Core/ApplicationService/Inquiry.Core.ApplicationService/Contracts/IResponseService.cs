using Inquiry.Core.Domain.Enums.Response;
using Inquiry.Core.Domain.Models.Response.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Core.ApplicationService.Contracts
{
    public interface IResponseService
    {
        ApiResponse<T> CreateSuccessResponse<T>(T data, string message = "عملیات با موفقیت انجام شد");
        ApiResponse<T> CreateErrorResponse<T>(string message, ResponseStatus statusCode = ResponseStatus.BadRequest);
        PagedResponse<T> CreatePagedResponse<T>(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords);
        ErrorResponse CreateValidationErrorResponse(Dictionary<string, string[]> errors);
    }
}
