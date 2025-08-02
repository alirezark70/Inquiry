using Inquiry.Infra.Resilience.Configuration;
using Inquiry.Infra.Resilience.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace Inquiry.Infra.Resilience.Registry
{
    public class PolicyRegistry : IPolicyRegistry
    {
        private readonly Dictionary<string, object> _pipelines = new();
        private readonly IOptions<ResilienceOptions> _options;
        private readonly ILogger<PolicyRegistry> _logger;

        public PolicyRegistry(IOptions<ResilienceOptions> options, ILogger<PolicyRegistry> logger)
        {
            _options = options;
            _logger = logger;
            InitializeDefaultPipelines();
        }

        public ResiliencePipeline<T> GetPipeline<T>(string policyName)
        {
            if (_pipelines.TryGetValue(policyName, out var pipeline) && pipeline is ResiliencePipeline<T> typedPipeline)
            {
                return typedPipeline;
            }

            throw new InvalidOperationException($"Pipeline '{policyName}' not found or type mismatch.");
        }

        public ResiliencePipeline GetPipeline(string policyName)
        {
            if (_pipelines.TryGetValue(policyName, out var pipeline) && pipeline is ResiliencePipeline untypedPipeline)
            {
                return untypedPipeline;
            }

            throw new InvalidOperationException($"Pipeline '{policyName}' not found.");
        }

        public void RegisterPipeline<T>(string policyName, ResiliencePipeline<T> pipeline)
        {
            _pipelines[policyName] = pipeline;
            _logger.LogInformation("Registered typed pipeline: {PolicyName}", policyName);
        }

        public void RegisterPipeline(string policyName, ResiliencePipeline pipeline)
        {
            _pipelines[policyName] = pipeline;
            _logger.LogInformation("Registered pipeline: {PolicyName}", policyName);
        }

        private void InitializeDefaultPipelines()
        {
            // Standard HTTP Pipeline
            var httpPipeline = CreateHttpPipeline();
            RegisterPipeline("http-standard", httpPipeline);

            // Database Pipeline
            var dbPipeline = CreateDatabasePipeline();
            RegisterPipeline("database", dbPipeline);

            // Critical Service Pipeline
            var criticalPipeline = CreateCriticalServicePipeline();
            RegisterPipeline("critical-service", criticalPipeline);
        }

        private ResiliencePipeline CreateHttpPipeline()
        {
            var options = _options.Value;

            return new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions
                {
                    MaxRetryAttempts = options.Retry.MaxRetryAttempts,
                    Delay = options.Retry.InitialDelay,
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = options.Retry.UseJitter,
                    OnRetry = args =>
                    {
                        _logger.LogWarning(
                            "Retry attempt {Attempt} after {Delay}ms",
                            args.AttemptNumber,
                            args.RetryDelay.TotalMilliseconds);
                        return ValueTask.CompletedTask;
                    }
                })
                .AddCircuitBreaker(new CircuitBreakerStrategyOptions
                {
                    FailureRatio = 0.5,
                    SamplingDuration = options.CircuitBreaker.SamplingDuration,
                    MinimumThroughput = options.CircuitBreaker.FailureThreshold,
                    BreakDuration = options.CircuitBreaker.BreakDuration,
                    OnOpened = args =>
                    {
                        _logger.LogError("Circuit breaker opened!");
                        return ValueTask.CompletedTask;
                    },
                    OnClosed = args =>
                    {
                        _logger.LogInformation("Circuit breaker closed!");
                        return ValueTask.CompletedTask;
                    }
                })
                .AddTimeout(options.Timeout.RequestTimeout)
                .Build();
        }

        private ResiliencePipeline CreateDatabasePipeline()
        {
            var options = _options.Value;

            return new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions
                {
                    MaxRetryAttempts = 2,
                    Delay = TimeSpan.FromMilliseconds(100),
                    BackoffType = DelayBackoffType.Linear,
                    ShouldHandle = new PredicateBuilder()
                        .Handle<DbUpdateException>()
                        .Handle<TimeoutException>()
                })
                .AddTimeout(TimeSpan.FromSeconds(5))
                .Build();
        }

        private ResiliencePipeline CreateCriticalServicePipeline()
        {
            var options = _options.Value;

            return new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions
                {
                    MaxRetryAttempts = 5,
                    Delay = TimeSpan.FromSeconds(2),
                    BackoffType = DelayBackoffType.Exponential,
                    MaxDelay = TimeSpan.FromSeconds(60),
                    UseJitter = true
                })
                .AddCircuitBreaker(new CircuitBreakerStrategyOptions
                {
                    FailureRatio = 0.3,
                    SamplingDuration = TimeSpan.FromMinutes(1),
                    MinimumThroughput = 10,
                    BreakDuration = TimeSpan.FromMinutes(2)
                })
                .AddTimeout(TimeSpan.FromSeconds(30))
                .AddConcurrencyLimiter(new ConcurrencyLimiterOptions
                {
                    PermitLimit = options.Bulkhead.MaxParallelization,
                    QueueLimit = options.Bulkhead.MaxQueuingActions
                })
                .Build();
        }
    }
}

