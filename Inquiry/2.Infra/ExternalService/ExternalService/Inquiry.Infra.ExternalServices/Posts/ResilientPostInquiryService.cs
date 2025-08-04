using Inquiry.Core.ApplicationService.Contracts.ExternalServices;
using Inquiry.Core.ApplicationService.Dtos.Posts;
using Inquiry.Infra.ExternalServices.Contracts;
using Inquiry.Infra.ExternalServices.Resilience.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Infra.ExternalServices.Posts
{
    public class ResilientPostInquiryService : IPostInquiryService
    {
        private readonly IPostInquiryService _innerService;
        private readonly IPolicyRegistry _policyRegistry;
        private readonly ILogger<ResilientPostInquiryService> _logger;
        private readonly ResiliencePipeline _pipeline;

        public ResilientPostInquiryService(ILogger<ResilientPostInquiryService> logger, IPolicyRegistry policyRegistry, IPostInquiryService innerService)
        {
            _logger = logger;
            _policyRegistry = policyRegistry;
            _innerService = innerService;
            _pipeline = _policyRegistry.GetPipeline(PolicyNames.PostInquiry);
        }

        public async Task<PostDto?> GetPostByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var context = ResilienceContextPool.Shared.Get(cancellationToken);

            try
            {
                context.Properties.Set(new ResiliencePropertyKey<string>("operation"), "GetPostById");

                return await _pipeline.ExecuteAsync(
                    async (ctx) =>
                    {
                        var cancellation = ctx.CancellationToken;
                        _logger.LogDebug("Executing GetPostById with resilience");
                        return await _innerService.GetPostByIdAsync(id, cancellation);
                    },
                    context);
            }
            catch (TimeoutRejectedException)
            {
                _logger.LogWarning("Request timed out for Id: {Id}", id);
                return GetFallbackPerson(id);
            }
            catch (BrokenCircuitException)
            {
                _logger.LogError("Circuit breaker is open for Post inquiry service");
                return GetFallbackPerson(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error in resilient Post inquiry");
                return GetFallbackPerson(id);
            }
            finally
            {
                ResilienceContextPool.Shared.Return(context);
            }
        }

        private PostDto? GetFallbackPerson(int id)
        {
            _logger.LogInformation("Returning fallback Post for Id: {Id}", id);

            return new PostDto(
                Id: 0,
                Title: "نامشخص",
                Body: "نامشخص",
                UserId: 0
            );
        }
    }
}
