using App.Metrics;
using App.Metrics.Counter;
using Inquiry.Infra.ExternalServices.Contracts;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Infra.ExternalServices.Telemetry
{
    public class ResilienceTelemetry : IResilienceTelemetry
    {
        private readonly ILogger<ResilienceTelemetry> _logger;
        private readonly IMetrics _metrics;

        public ResilienceTelemetry(ILogger<ResilienceTelemetry> logger, IMetrics metrics)
        {
            _logger = logger;
            _metrics = metrics;
        }

        public void RecordRetryAttempt(string policyName, int attemptNumber, TimeSpan delay)
        {
            _logger.LogInformation(
                "Retry attempt {AttemptNumber} for policy {PolicyName} after {Delay}ms",
                attemptNumber, policyName, delay.TotalMilliseconds);

            _metrics.Measure.Counter.Increment(
                new CounterOptions { Name = "resilience_retry_attempts", Tags = new MetricTags("policy", policyName) });
        }

        public void RecordCircuitBreakerStateChange(string policyName, CircuitState newState)
        {
            _logger.LogWarning(
                "Circuit breaker {PolicyName} changed state to {NewState}",
                policyName, newState);

            _metrics.Measure.Counter.Increment(
                new CounterOptions
                {
                    Name = "resilience_circuit_breaker_state_changes",
                    Tags = new MetricTags(new[] { "policy", "state" }, new[] { policyName, newState.ToString() })
                });
        }

        public void RecordTimeout(string policyName, TimeSpan elapsed)
        {
            _logger.LogWarning(
                "Timeout occurred for policy {PolicyName} after {Elapsed}ms",
                policyName, elapsed.TotalMilliseconds);

            _metrics.Measure.Counter.Increment(
                new CounterOptions { Name = "resilience_timeouts", Tags = new MetricTags("policy", policyName) });
        }

        public void RecordFallback(string policyName, string reason)
        {
            _logger.LogInformation(
                "Fallback executed for policy {PolicyName}. Reason: {Reason}",
                policyName, reason);

            _metrics.Measure.Counter.Increment(
                new CounterOptions
                {
                    Name = "resilience_fallbacks",
                    Tags = new MetricTags(new[] { "policy", "reason" }, new[] { policyName, reason })
                });
        }
    }

}
