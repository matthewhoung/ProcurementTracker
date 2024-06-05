using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.Configurations
{
    public static class MySQLConnectionConfiguration
    {
        public static IServiceCollection AddMySQLConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(provider =>
                    new MySqlConnection(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
