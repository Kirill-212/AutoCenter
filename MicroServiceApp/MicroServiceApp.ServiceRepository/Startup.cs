using MicroServiceApp.InfrastructureLayer.ConsulSettings;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using MicroServiceApp.ServiceRepository.Repository;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IApplicationLifetime = Microsoft.AspNetCore.Hosting.IApplicationLifetime;
using Microsoft.EntityFrameworkCore;
using MicroServiceApp.InfrastructureLayer.Auth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MicroServiceApp.ServiceRepository
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
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //         .AddJwtBearer(options =>
            //         {
            //             options.RequireHttpsMetadata = false;
            //             options.TokenValidationParameters = new TokenValidationParameters
            //             {
            //                 ValidateIssuer = true,
            //                 ValidIssuer = AuthOptions.ISSUER,
            //                 ValidateAudience = true,
            //                 ValidAudience = AuthOptions.AUDIENCE,
            //                 ValidateLifetime = true,
            //                 IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            //                 ValidateIssuerSigningKey = true,
            //             };
            //         });
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ContextDb>(options =>
                options.UseSqlServer(connection));
            services.AddControllers();
            services.AddTransient<IAsyncRepositoryUser<User>, AsyncRepositoryUser>();
            services.AddTransient<IAsyncRepositoryOrder<Order>, AsyncRepositoryOrder>();
            services.AddTransient<IAsyncRepositoryNew<New>, AsyncRepositoryNew>();
            services.AddTransient<IAsyncRepositoryImg<Img>, AsyncRepositoryImg>();
            services.AddTransient<
                IAsyncRepositoryEmployee<Employee>,
                AsyncRepositoryEmployee>();
            services.AddTransient<
                IAsyncRepositoryClientCar<ClientCar>,
                AsyncRepositoryClientCar>();
            services.AddTransient<IAsyncRepositoryCar<Car>, AsyncRepositoryCar>();
            services.AddTransient<
                IAsyncRepositoryActionCar<ActionCar>,
                AsyncRepositoryActionCar>();
            services.AddTransient<
               IAsyncRepositoryTestDrive<TestDrive>,
               AsyncRepositoryTestDrive>();
            services.AddTransient<IAsyncRepositoryRole<Role>, AsyncRepositoryRole>();
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
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

            app.UseAuthentication();
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
