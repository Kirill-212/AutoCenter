using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System;

namespace MicroServiceApp.ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            services.AddControllers();
            services.AddOcelot(
                new ConfigurationBuilder()
                .AddJsonFile($"ocelot.{env}.json")
                .Build()
                ).AddConsul();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            await app.UseOcelot();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(
 options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
 );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
