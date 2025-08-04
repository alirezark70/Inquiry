using Inquiry.Infra.Resilience.Configuration;
using Inquiry.Infra.Resilience.Contracts;
using Inquiry.Infra.Resilience.Registry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Microsoft.Extensions.Http;

namespace Inquiry.Infra.Extenstions.DependencyInjection
{
  
    public static IServiceCollection ResilientServiceRegistrer(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            // Register Options
            services.Configure<ResilienceOptions>(
                configuration.GetSection(ResilienceOptions.SectionName));

            // Register Policy Registry
            services.AddSingleton<IPolicyRegistry, PolicyRegistry>();

            // Register HTTP Clients with Resilience
            services.AddHttpClient<IPostInquiryService, PostInquiryService>(client =>
            {
                client.BaseAddress = new Uri(configuration["ExternalServices:PersonApi:BaseUrl"]!);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddStandardResilienceHandler();

            // Register Decorated Service
            services.Decorate<IPostInquiryService, ResilientPostInquiryService>();

            // Register Custom Resilience Pipelines
            services.AddResiliencePipeline(PolicyNames.PostInquiry, (builder, context) =>
            {
                var options = context.ServiceProvider
                    .GetRequiredService<IOptions<ResilienceOptions>>().Value;

                builder
                    .AddRetry(new RetryStrategyOptions
                    {
                        MaxRetryAttempts = options.Retry.MaxRetryAttempts,
                        Delay = options.Retry.InitialDelay,
                        BackoffType = DelayBackoffType.Exponential,
                        UseJitter = options.Retry.UseJitter,
                        OnRetry = args =>
                        {
                            var logger = context.ServiceProvider.GetRequiredService<ILogger<PolicyRegistry>>();
                            logger.LogWarning(
                                "PersonInquiry Retry {Attempt} after {Delay}ms",
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
                        BreakDuration = options.CircuitBreaker.BreakDuration
                    })
                    .AddTimeout(options.Timeout.RequestTimeout);
            });

            return services;
        

    }
}
