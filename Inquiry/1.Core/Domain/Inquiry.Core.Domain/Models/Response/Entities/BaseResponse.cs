using Inquiry.Core.Domain.Enums.Response;
using Inquiry.Core.Domain.Models.Response.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Core.Domain.Models.Response.Entities
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public ResponseStatus StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public  DateTime Timestamp { get; set; }=DateTime.UtcNow;
        public string TraceId { get; set; } = Guid.NewGuid().ToString();
    }
}
