using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace NET.Mysql.Sample.WebApi.Extensions
{
    public static class HealthChecks
    {
        public static IServiceCollection AddAppHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddMySql(
                   connectionString: configuration["ConnectionStrings:MySQL"],
                   name: "MySql",
                   failureStatus: HealthStatus.Degraded,
                   tags: new string[] { "ready", "mysql" });

            return services;
        }

        public static IEndpointRouteBuilder MapAppHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
            {
                Predicate = (check) => check.Tags.Contains("ready"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
            {
                Predicate = (_) => false
            });

            return endpoints;
        }
    }
}
