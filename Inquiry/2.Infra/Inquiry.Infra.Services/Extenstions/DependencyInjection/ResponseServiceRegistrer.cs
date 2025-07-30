using Inquiry.Core.ApplicationService.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Infra.Services.Extenstions.DependencyInjection
{
    public static class ResponseServiceRegistrer
    {
        public static IServiceCollection RegisterSimpleDateTimeService(this IServiceCollection services)
        {
            services.AddTransient<IResponseService, ResponseService>();

            return services;
        }
    }
}
