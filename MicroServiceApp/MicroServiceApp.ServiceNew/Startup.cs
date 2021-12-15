using AutoMapper;
using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.AutoMapper;
using MicroServiceApp.InfrastructureLayer.ConsulSettings;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceNew.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using IApplicationLifetime = Microsoft.AspNetCore.Hosting.IApplicationLifetime;

namespace MicroServiceApp.ServiceNew
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
            services.AddCors();
            services.AddTransient<
                IAsyncHttpClientNew<New>,
                AsyncHttpClientForNewService<New>>();
            services.AddTransient<
                IAsyncHttpClientUserForNew<Employee>, 
                AsyncHttpClientForNewService<Employee>>();
            services.AddTransient<
                IAsyncHttlClientImg<Img>, 
                AsyncHttpClientForNewService<Img>>();
            services.AddTransient<IAsyncServiceNew<New>, AsyncServiceNew>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllers();
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

          //  app.UseHttpsRedirection();

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
