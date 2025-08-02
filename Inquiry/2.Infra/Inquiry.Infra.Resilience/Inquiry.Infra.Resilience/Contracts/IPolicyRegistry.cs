using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Infra.Resilience.Contracts
{
    public interface IPolicyRegistry
    {
        ResiliencePipeline<T> GetPipeline<T>(string policyName);
        ResiliencePipeline GetPipeline(string policyName);
        void RegisterPipeline<T>(string policyName, ResiliencePipeline<T> pipeline);
        void RegisterPipeline(string policyName, ResiliencePipeline pipeline);
    }
}
