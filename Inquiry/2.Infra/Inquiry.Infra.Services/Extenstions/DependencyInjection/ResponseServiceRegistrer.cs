using Inquiry.Core.ApplicationService.Contracts;
using Inquiry.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Inquiry.Infra.Extenstions.DependencyInjection
{
    public static class ResponseServiceRegistrer
    {
        public static IServiceCollection RegisterResponseService(this IServiceCollection services)
        {
            services.AddTransient<IResponseService, ResponseService>();

            return services;
        }
    }
}
