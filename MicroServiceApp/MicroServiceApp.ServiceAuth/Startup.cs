using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.ConsulSettings;
using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceAuth.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using IApplicationLifetime = Microsoft.AspNetCore.Hosting.IApplicationLifetime;

namespace MicroServiceApp.ServiceAuth
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
            services.AddControllers();
            services.AddOptions();
            services.AddTransient<
                IAsyncHttpClientUser<User>,
                AsyncHttpClientForUserService<User>>();
            services.AddTransient<
                IAsyncServiceVerifyUser<string, AuthDto, User>,
                AsyncServiceVerifyUser>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
         //   app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(
 options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
 );

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var consulOption = new ConsulOption
            {
                ServiceName = Configuration["ServiceName"],
                ServiceIP = Configuration["ServiceIP"],
                ServicePort = Convert.ToInt32(Configuration["ServicePort"]),
                ServiceHealthCheck = Configuration["ServiceHealthCheck"],
                Address = Configuration["ConsulAddress"]
            };
            app.RegisterConsul(lifetime, consulOption);
        }
    }
}
