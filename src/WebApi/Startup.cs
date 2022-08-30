using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NET.Mysql.Sample.Application.DependencyInjection;
using NET.Mysql.Sample.Application.UseCases.CreateContact;
using NET.Mysql.Sample.Infrastructure.MySQL.DependencyInjection;
using NET.Mysql.Sample.WebApi.Extensions;
using NET.Mysql.Sample.WebApi.Filters;
using NET.Mysql.Sample.WebApi.Middleware;
using System;

namespace NET.Mysql.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvcCore(opt =>
            {
                opt.Filters.Add(typeof(ModelStateValidationActionFilter));
            });

            services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = true; });

            // validators
            services.AddFluentValidation();
            services.AddTransient<IValidator<CreateContactInput>, CreateContactValidator>();

            services
                .AddApplicationMappers()
                .AddMySqlConfig(Configuration)
                .AddMediatR(AppDomain.CurrentDomain.GetAssemblies())
                .AddRouting(opt => opt.LowercaseUrls = true)
                .AddSwagger()
                .AddAppHealthChecks(Configuration)
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwaggerConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
                endpoints.MapAppHealthChecks();
            });
        }
    }
}
