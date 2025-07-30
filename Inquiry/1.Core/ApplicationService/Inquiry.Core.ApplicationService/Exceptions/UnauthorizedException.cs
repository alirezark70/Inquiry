using Inquiry.Core.Domain.Enums.Response;

namespace Inquiry.Core.ApplicationService.Exceptions
{
    public class UnauthorizedException: ApiException
    {
        public UnauthorizedException(string message = "دسترسی غیرمجاز")
        : base(message, ResponseStatus.Unauthorized)
        {
        }
    }
}
