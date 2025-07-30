using Inquiry.Core.Domain.Enums.Response;

namespace Inquiry.Core.ApplicationService.Exceptions
{
    public class ValidationException: ApiException
    {
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException(Dictionary<string, string[]> errors)
            : base("یک یا چند خطای اعتبارسنجی رخ داده است", ResponseStatus.UnprocessableEntity)
        {
            Errors = errors;
        }
    }
}
