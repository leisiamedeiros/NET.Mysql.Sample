using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NET.Mysql.Sample.Domain.Interfaces;
using NET.Mysql.Sample.Infrastructure.MySQL.Repositories;

namespace NET.Mysql.Sample.Infrastructure.MySQL.DependencyInjection
{
    public static class MySqlExtension
    {
        public static IServiceCollection AddMySqlConfig(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:MySQL"];

            AddRepositories(services, connectionString);

            return services;
        }

        private static void AddRepositories(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IGetAllContactsRepository, ContactRepository>(_ => new ContactRepository(connectionString));
            services.AddScoped<ICreateContactRepository, ContactRepository>(_ => new ContactRepository(connectionString));
        }
    }
}
