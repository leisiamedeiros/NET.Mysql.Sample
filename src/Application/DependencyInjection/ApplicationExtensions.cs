using Microsoft.Extensions.DependencyInjection;
using NET.Mysql.Sample.Application.Mappers;

namespace NET.Mysql.Sample.Application.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(GetAllContactsOutputProfile), typeof(CreateContactInputProfile)
            );

            return services;
        }
    }
}
