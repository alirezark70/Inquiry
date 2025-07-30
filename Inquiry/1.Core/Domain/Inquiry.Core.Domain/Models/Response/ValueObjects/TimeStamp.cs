using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Core.Domain.Models.Response.ValueObjects
{
    public record TimeStamp
    {
        public DateTime DateNowUtc { get; set; } = DateTime.UtcNow;
    }

    
}
