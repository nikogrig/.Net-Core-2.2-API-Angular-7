using Microsoft.Extensions.DependencyInjection;
using Rubi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rubi.Infrastructure.ServiceExtension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            Assembly.GetAssembly(typeof(IService))
                .GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces()
                                          .Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Impementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Impementation));

            return services;
        }
    }
}