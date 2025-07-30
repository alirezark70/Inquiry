using Inquiry.Core.Domain.Enums.Response;

namespace Inquiry.Core.ApplicationService.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string entityName, object key)
        : base($"{entityName} با شناسه {key} یافت نشد", ResponseStatus.NotFound)
        {
        }
    }
}
