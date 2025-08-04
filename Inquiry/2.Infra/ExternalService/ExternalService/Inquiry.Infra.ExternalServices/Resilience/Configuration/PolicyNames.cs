using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Infra.ExternalServices.Resilience.Configuration
{
    public static class PolicyNames
    {
        public const string HttpStandard = "http-standard";
        public const string Database = "database";
        public const string CriticalService = "critical-service";
        public const string PostInquiry = "person-inquiry";
        public const string ExternalApi = "external-api";
    }
}
