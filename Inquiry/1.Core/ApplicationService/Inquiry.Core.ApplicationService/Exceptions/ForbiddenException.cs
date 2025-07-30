using Inquiry.Core.Domain.Enums.Response;

namespace Inquiry.Core.ApplicationService.Exceptions
{
    public class ForbiddenException :ApiException
    {
        public ForbiddenException(string message = "شما اجازه دسترسی به این منبع را ندارید")
        : base(message, ResponseStatus.Forbidden)
        {
        }
    }
}
