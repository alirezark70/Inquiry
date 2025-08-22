using FluentValidation;
using Inquiry.Core.ApplicationService.Behaviors;
using Inquiry.Core.ApplicationService.Contracts;
using Inquiry.Core.ApplicationService.Mapping.Contracts;
using Inquiry.Core.ApplicationService.Services.Mapping;
using Inquiry.Core.ApplicationService.Services.Response;
using Mapster;
using MapsterMapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inquiry.Core.Exceptions.DependencyInjection
{
    public static class ResponseServiceRegistrer
    {
        public static IServiceCollection RegisterResponseService(this IServiceCollection services)
        {
            services.AddTransient<IResponseService, ResponseService>();


            // Mapster Configuration
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(Assembly.GetAssembly(typeof(IMappingConfig))!);
            typeAdapterConfig.Default.PreserveReference(true);

            // Add Mapster
            services.AddSingleton(typeAdapterConfig);
            services.AddScoped<IMapper, ServiceMapper>();
            services.AddScoped<IMappingService, MappingService>();


            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                // ثبت رفتارها به ترتیب اجرا
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            });


            return services;
        }
    }
}
