using Inquiry.Core.ApplicationService.Behaviors;
using Inquiry.Core.ApplicationService.Contracts;
using Inquiry.Core.Domain.Enums.Response;
using Inquiry.Core.Domain.Models.Response.Entities;
using Inquiry.EndPoints.RestApi.Filters;
using Inquiry.EndPoints.RestApi.Middleware;
using Inquiry.Infra.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inquiry.EndPoints.RestApi.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddResponseFramework(this IServiceCollection services)
        {
            // Add Response Service
            services.AddScoped<IResponseService, ResponseService>();

            // Add MediatR Pipeline Behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Add Filters
            services.AddControllers(options =>
            {
                options.Filters.Add<ApiResponseActionFilter>();
                options.Filters.Add<GlobalExceptionFilter>();
            });

            // Configure API Behavior Options
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var response = new ErrorResponse("خطای اعتبارسنجی", ResponseStatus.UnprocessableEntity);
                    foreach (var error in errors)
                    {
                        foreach (var message in error.Value)
                        {
                            response.AddError(error.Key, message);
                        }
                    }

                    return new UnprocessableEntityObjectResult(response);
                };
            });

            return services;
        }

        public static IApplicationBuilder UseResponseFramework(this IApplicationBuilder app)
        {
            
             app.UseMiddleware<ResponseWrapperMiddleware>();

            return app;
        }
    }
}
