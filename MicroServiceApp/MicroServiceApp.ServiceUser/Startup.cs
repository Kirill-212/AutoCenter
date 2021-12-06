using AutoMapper;
using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.AutoMapper;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceUser.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Hosting;
using IApplicationLifetime = Microsoft.AspNetCore.Hosting.IApplicationLifetime;
using MicroServiceApp.InfrastructureLayer.ConsulSettings;

namespace MicroServiceApp.ServiceUser
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<
                IAsyncHttpClientRole<Role>,
                AsyncHttpClientForUserService<Role>>();
            services.AddTransient<
                IAsyncHttpClientUser<User>,
                AsyncHttpClientForUserService<User>>();
            services.AddTransient<
                IAsyncHttpClientEmployee<Employee>,
                AsyncHttpClientForUserService<Employee>>();
            services.AddTransient<IAsyncServiceUser<User>, AsyncServiceUser>();
            services.AddTransient<
                IAsyncServiceEmployee<Employee>,
                AsyncServiceEmployee>();
            services.AddTransient<IAsyncServiceRole<Role>, AsyncServiceRole>();
            services.AddControllers();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApplicationLifetime lifetime
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            var consulOption = new ConsulOption
            {
                ServiceName = Configuration["ServiceName"],
                ServiceIP = Configuration["ServiceIP"],
                ServicePort = Convert.ToInt32(Configuration["ServicePort"]),
                ServiceHealthCheck = Configuration["ServiceHealthCheck"],
                Address = Configuration["ConsulAddress"]
            };
            app.RegisterConsul(lifetime, consulOption);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
