using CleanArchCqrs.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchCqrs.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            //services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssemblies(assembly);
            //    cfg.AddBehavior<ValidationBehavior<,>>();
            //    cfg.AddBehavior<LogginingBehavior<,>>();
            //    cfg.AddBehavior<PerformanceBehavior<,>>();
            //});

            //services.AddValidatorsFromAssembly(assembly);
            //services.AddAutoMapper(assembly);

            return services;
        }
    }
}
