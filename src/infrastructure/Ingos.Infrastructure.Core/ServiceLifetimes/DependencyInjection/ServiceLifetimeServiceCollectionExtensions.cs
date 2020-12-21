//-----------------------------------------------------------------------
// <copyright file= "ServiceLifetimeServiceCollectionExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/5/16 9:37:18
// Modified by:
// Description: Custom service injection extension method
//-----------------------------------------------------------------------

using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Ingos.Infrastructure.Core.ServiceLifetimes.Contracts;
using Microsoft.Extensions.DependencyModel;

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
            // Get assemblies
            //
            var libraries = DependencyContext.Default.RuntimeLibraries
                .Where(i => !i.Serviceable && i.Type != "package");

            var assemblies = libraries
                .Select(i => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(i.Name))).ToList();

            // Inject services
            //
            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes
                    => classes.AssignableTo<ITransientService>()
                )
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                .AddClasses(classes
                    => classes.AssignableTo<IScopedService>()
                )
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes
                    => classes.AssignableTo<ISingletonService>()
                )
                .AsImplementedInterfaces()
                .WithSingletonLifetime()
            );

            return services;
        }
    }
}