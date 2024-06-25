using Application.Services;
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
            services.AddScoped<FormInfoService>();
            services.AddScoped<FilteredFormsService>();
            services.AddScoped<PaymentCalculationService>();
            services.AddScoped<FormInfoByStageService>();

            return services;
        }
    }
}
