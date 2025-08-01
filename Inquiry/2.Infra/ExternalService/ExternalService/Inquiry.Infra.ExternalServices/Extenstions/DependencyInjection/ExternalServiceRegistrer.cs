using Inquiry.Core.ApplicationService.Contracts;
using Inquiry.Infra.ExternalServices;
using Microsoft.Extensions.DependencyInjection;

namespace Inquiry.Infra.Extenstions.DependencyInjection
{
    public static class ExternalServiceRegistrer
    {
        public static IServiceCollection RegisterExternalService(this IServiceCollection services)
        {
            //services.AddTransient<IResponseService, ResponseService>();

            return services;
        }
    }
}
