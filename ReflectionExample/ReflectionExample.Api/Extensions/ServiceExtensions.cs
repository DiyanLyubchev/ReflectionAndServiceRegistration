﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReflectionExample.Domain.Interfaces.LifetimeRegistrationOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReflectionExample.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Define types that need matching
            Type scopedService = typeof(IScopedService);
            Type singletonService = typeof(ISingletonService);
            Type transientService = typeof(ITransientService);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => scopedService.IsAssignableFrom(p) || transientService.IsAssignableFrom(p) || singletonService.IsAssignableFrom(p) && !p.IsInterface)
                    .Select(s => new
                    {
                        Service = s.GetInterface($"I{s.Name}"),
                        Implementation = s
                    }).Where(x => x.Service != null);

            foreach (var type in types)
            {
                if (scopedService.IsAssignableFrom(type.Service))
                {
                    services.AddScoped(type.Service, type.Implementation);
                }

                if (transientService.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }

                if (singletonService.IsAssignableFrom(type.Service))
                {
                    services.AddSingleton(type.Service, type.Implementation);
                }
            }
        }
    }
}