namespace Inquiry.Core.Domain.Models.Response.Entities
{
    public class ErrorDetail
    {
        public string Field { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? Code { get; set; }
    }
}

