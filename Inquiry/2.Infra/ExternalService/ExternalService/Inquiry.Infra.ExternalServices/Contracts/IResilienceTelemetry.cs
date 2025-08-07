using Inquiry.Core.Domain.Enums.Base;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Infra.ExternalServices.Contracts
{
    public interface IResilienceTelemetry
    {
        void RecordRetryAttempt(PolicyType policyType, int attemptNumber, TimeSpan delay);
        void RecordCircuitBreakerStateChange(PolicyType policyType, CircuitState newState);
        void RecordTimeout(PolicyType policyType, TimeSpan elapsed);
        void RecordFallback(PolicyType policyType, string reason);
    }
}
