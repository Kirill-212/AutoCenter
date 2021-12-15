﻿using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace MicroServiceApp.InfrastructureLayer.ConsulSettings
{
    public static class ConsulBuilderExtensions
    {
        public static IApplicationBuilder RegisterConsul(
            this IApplicationBuilder app,
            IApplicationLifetime lifetime,
            ConsulOption consulOption)
        {
            var consulClient = new ConsulClient(x =>
            {
                x.Address = new Uri(consulOption.Address);
            });

            var registration = new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(),
                Name = consulOption.ServiceName,
                Address = consulOption.ServiceIP,
                Port = consulOption.ServicePort,
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(5),
                    HTTP = consulOption.ServiceHealthCheck,
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };

            consulClient.Agent.ServiceRegister(registration).Wait();

            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

            return app;
        }
    }
}
