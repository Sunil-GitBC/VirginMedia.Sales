
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VirginMedia.Sales.Domain.Repositories;
using VirginMedia.Sales.Infrastructure.Repositories;

namespace VirginMedia.Sales.Application
{
    public static class ApplicationRegistrationServices
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddSingleton(typeof(IProductSalesRepository), typeof(ProductSalesCSVRepository));
            services.AddAutoMapper(assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            return services;
        }
    }
}

