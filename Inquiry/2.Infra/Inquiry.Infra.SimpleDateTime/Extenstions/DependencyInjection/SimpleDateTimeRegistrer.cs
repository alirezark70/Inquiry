using Inquiry.Core.ApplicationService.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Infra.SimpleDateTime.Extenstions.DependencyInjection
{
    public static class SimpleDateTimeRegistrer
    {
        public static IServiceCollection RegisterSimpleDateTimeService(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
