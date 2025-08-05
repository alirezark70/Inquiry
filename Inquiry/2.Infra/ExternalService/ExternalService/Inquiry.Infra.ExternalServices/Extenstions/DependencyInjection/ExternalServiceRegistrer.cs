using Inquiry.Core.ApplicationService.Contracts;
using Inquiry.Core.ApplicationService.Contracts.ExternalServices;
using Inquiry.Infra.ExternalServices;
using Inquiry.Infra.ExternalServices.Contracts;
using Inquiry.Infra.ExternalServices.Posts;
using Inquiry.Infra.ExternalServices.Resilience.Configuration;
using Inquiry.Infra.ExternalServices.Resilience.Registry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System;
using System.Configuration;

namespace Inquiry.Infra.Extenstions.DependencyInjection
{
    public static class ExternalServiceRegistrer
    {
        public static IServiceCollection RegisterExternalService(this IServiceCollection services, IConfiguration configuration)
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
            .InjectStandardResilienceHandler();

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

        public static IHttpClientBuilder InjectStandardResilienceHandler(
       this IHttpClientBuilder builder)
        {

            builder.AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = 3;
                options.Retry.Delay = TimeSpan.FromSeconds(1);
                options.Retry.BackoffType = DelayBackoffType.Exponential;
                options.Retry.UseJitter = true;

                options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(30);
                options.CircuitBreaker.FailureRatio = 0.5;
                options.CircuitBreaker.MinimumThroughput = 5;
                options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(30);

                options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(10);
                options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(30);
            });

            return builder;
        }
    }
}
