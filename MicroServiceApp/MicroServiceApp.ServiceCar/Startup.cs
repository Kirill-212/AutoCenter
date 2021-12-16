using AutoMapper;
using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.HttpClientLayer.ServiceCar;
using MicroServiceApp.InfrastructureLayer.Auth;
using MicroServiceApp.InfrastructureLayer.AutoMapper;
using MicroServiceApp.InfrastructureLayer.ConsulSettings;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceCar.ContextDB;
using MicroServiceApp.ServiceCar.Repository;
using MicroServiceApp.ServiceCar.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using IApplicationLifetime = Microsoft.AspNetCore.Hosting.IApplicationLifetime;

namespace MicroServiceApp.ServiceCar
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
            services.AddCors(); services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddSingleton(
                new ContextDb(
                    Configuration.GetConnectionString("DefaultConnection"),
                    Configuration["IdForm"]
                    )
                );
            services.AddTransient<
                IAsyncRepositoryCarEquipmentForm<CarEquipmentForm>, 
                AsyncRepositoryCarEquipmentForm>();
            services.AddTransient<
                IAsyncServiceCarEquipmentForm<CarEquipmentForm>,
                AsyncServiceCarEquipmentForm>();
            services.AddTransient<
                IAsyncRepositoryCarEquipment<CarEquipment>, 
                AsyncRepositoryCarEquipment>();
            services.AddTransient<
                IAsyncServiceCarEquipment<CarEquipment>,
                AsyncServiceCarEquipment>();
            services.AddTransient<
                IAsyncHttpClientCar<Car>, 
                AsyncHttpClientForCarService<Car>>();
            services.AddSingleton<IAsyncServiceCar<Car>, AsyncServiceCar>();
            services.AddTransient<
                IAsyncHttpClientActionCar<ActionCar>,
                AsyncHttpClientForCarService<ActionCar>>();
            services.AddTransient<
                IAsyncServiceActionCar<ActionCar>,
                AsyncServiceActionCar>();
            services.AddTransient<
                IAsyncHttpClientOrder<Order>,
                AsyncHttpClientForCarService<Order>>();
            services.AddTransient<
                IAsyncServiceOrder<Order>,
                AsyncServiceOrder>();
            services.AddTransient<
                IAsyncHttpClientClientCar<ClientCar>,
                AsyncHttpClientForCarService<ClientCar>>();
            services.AddTransient<
                IAsyncHttpClientUser<User>,
                AsyncHttpClientForUserService<User>>();
            services.AddTransient<
                IAsyncServiceClientCar<ClientCar>,
                AsyncServiceClientCar>();
            services.AddTransient<
              IAsyncHttpClientTestDrive<TestDrive>,
              AsyncHttpClientForCarService<TestDrive>>();
            services.AddTransient<
             IAsyncServiceTestDrive<TestDrive>,
             AsyncServiceTestDrive>();
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
         //   app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(
             options => options.WithOrigins("*")
             .AllowAnyMethod().AllowAnyHeader()
             );

            app.UseAuthentication();
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
