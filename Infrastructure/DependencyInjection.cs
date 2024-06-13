using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure

{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<IFileUploaderRepository, FileUploaderRepository>();
            services.AddScoped<IGenericRepository, GenericRepository>();

            return services;
        }
    }
}
