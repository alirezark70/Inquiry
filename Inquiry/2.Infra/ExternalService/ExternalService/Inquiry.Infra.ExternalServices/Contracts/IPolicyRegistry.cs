using Inquiry.Core.Domain.Enums.Base;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Infra.ExternalServices.Contracts
{
    public interface IPolicyRegistry
    {
        ResiliencePipeline<T> GetPipeline<T>(PolicyType policyType);
        ResiliencePipeline GetPipeline(PolicyType policyType);
        void RegisterPipeline<T>(PolicyType policyType, ResiliencePipeline<T> pipeline);
        void RegisterPipeline(PolicyType policyType, ResiliencePipeline pipeline);
    }
}
