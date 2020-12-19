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
using Ingos.Infrastructure.Core.ServiceLifetimes.Contracts;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceLifetimeServiceCollectionExtensions
    {
        /// <summary>
        ///     Dependency inject custom service
        /// </summary>
        /// <param name="services">The instance of <see cref="IServiceCollection" /></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Get all assemblies
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes
                    => classes.AssignableTo<ITransientService>()
                )
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                .AddClasses(classes
                    => classes.AssignableTo<IScopedService>()
                )
                .As<IScopedService>()
                .WithScopedLifetime()
                .AddClasses(classes
                    => classes.AssignableTo<ISingletonService>()
                )
                .As<ISingletonService>()
                .WithSingletonLifetime()
            );

            return services;
        }
    }
}