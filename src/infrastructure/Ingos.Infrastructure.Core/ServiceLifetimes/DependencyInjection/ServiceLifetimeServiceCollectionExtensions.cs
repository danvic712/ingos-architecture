//-----------------------------------------------------------------------
// <copyright file= "ServiceLifetimeServiceCollectionExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/5/16 9:37:18
// Modified by:
// Description: Custom service injection extension method
//-----------------------------------------------------------------------

using System;
using System.Linq;
using Ingos.Infrastructure.Core.ServiceLifetimes;
using Ingos.Infrastructure.Core.ServiceLifetimes.Contracts;
using Ingos.Infrastructure.Core.ServiceLifetimes.Exceptions;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceLifetimeServiceCollectionExtensions
    {
        /// <summary>
        ///     Dependency inject custom service
        /// </summary>
        /// <param name="services">The instance of <see cref="IServiceCollection" /></param>
        /// <param name="setupAction">The inject service's config options</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            Action<ApplicationServiceOptions> setupAction)
        {
            if (setupAction == null)
                throw new ArgumentNullException(nameof(setupAction));

            var options = new ApplicationServiceOptions();
            setupAction.Invoke(options);

            return InjectServiceCore(services, options);
        }

        /// <summary>
        ///     Core method of inject service
        /// </summary>
        /// <param name="services">The services collection</param>
        /// <param name="options">The inject service's config options</param>
        /// <returns></returns>
        private static IServiceCollection InjectServiceCore(IServiceCollection services,
            ApplicationServiceOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (!options.Assembly.Any())
                return services;

            var transient = typeof(ITransient);
            var scoped = typeof(IScoped);
            var singleton = typeof(ISingleton);

            foreach (var assembly in options.Assembly)
                foreach (var implement in assembly.GetTypes())
                {
                    var interfaceType = implement.GetInterfaces();

                    var lifetime = interfaceType
                        .FirstOrDefault(i => i == transient || i == scoped || i == singleton);

                    if (lifetime == null)
                        throw new ServiceLifetimeException($"Did not find this {nameof(implement)}'s lifetime");

                    foreach (var service in interfaceType)
                        switch (lifetime.Name)
                        {
                            case "ITransient":
                                services.AddTransient(service, implement);
                                break;

                            case "IScoped":
                                services.AddScoped(service, implement);
                                break;

                            case "ISingleton":
                                services.AddSingleton(service, implement);
                                break;
                        }
                }

            return services;
        }
    }
}