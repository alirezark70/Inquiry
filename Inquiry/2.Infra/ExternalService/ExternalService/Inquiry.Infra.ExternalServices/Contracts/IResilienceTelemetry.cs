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
        void RecordRetryAttempt(string policyName, int attemptNumber, TimeSpan delay);
        void RecordCircuitBreakerStateChange(string policyName, CircuitState newState);
        void RecordTimeout(string policyName, TimeSpan elapsed);
        void RecordFallback(string policyName, string reason);
    }
}
