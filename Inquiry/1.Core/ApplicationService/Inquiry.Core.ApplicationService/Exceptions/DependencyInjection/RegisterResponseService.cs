using Inquiry.Core.ApplicationService.Behaviors;
using Inquiry.Core.ApplicationService.Contracts;
using Inquiry.Core.ApplicationService.Mapping.MapperProfile;
using Inquiry.Core.ApplicationService.Services.Response;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR.Pipeline;

namespace Inquiry.Core.Exceptions.DependencyInjection
{
    public static class ResponseServiceRegistrer
    {
        public static IServiceCollection RegisterResponseService(this IServiceCollection services)
        {
            services.AddTransient<IResponseService, ResponseService>();

            services.AddAutoMapper(cfg =>
            {
                //cfg.AddMaps(Assembly.GetExecutingAssembly());
               
                 cfg.AddProfile<MappingProfile>();
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                // ثبت رفتارها به ترتیب اجرا
                cfg.AddBehavior(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            });


            return services;
        }
    }
}
