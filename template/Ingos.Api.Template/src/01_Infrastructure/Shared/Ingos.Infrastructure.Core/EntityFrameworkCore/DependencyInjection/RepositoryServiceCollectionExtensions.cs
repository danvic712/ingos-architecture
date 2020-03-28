//-----------------------------------------------------------------------
// <copyright file= "RepositoryServiceCollectionExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 22:40:32
// Modified by:
// Description: EFCore services injection extension method
//-----------------------------------------------------------------------
using Ingos.Infrastructure.Core.EntityFrameworkCore.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ingos.Infrastructure.Core.EntityFrameworkCore.DependencyInjection
{
    public static class RepositoryServiceCollectionExtensions
    {
        /// <summary>
        /// Inject database repository into service collection
        /// </summary>
        /// <param name="services">The services that need to be injected into the container <see cref="IServiceCollection"/></param>
        /// <param name="setupAction">The instance of Ingos Repository config options</param>
        /// <returns></returns>
        public static IServiceCollection AddIngosApplicationRepository(this IServiceCollection services,
            Action<IngosRepositoryConfigurationOptions> setupAction)
        {
            if (setupAction == null)
                throw new ArgumentNullException(nameof(setupAction));

            // Get config options
            //
            var options = new IngosRepositoryConfigurationOptions();
            setupAction?.Invoke(options);

            return AddRepositoryService(services, options);
        }

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="services">The instance of service collections</param>
        /// <param name="options">The instance of Ingos Repository config options</param>
        /// <returns></returns>
        private static IServiceCollection AddRepositoryService(IServiceCollection services,
            IngosRepositoryConfigurationOptions options)
        {
            // Get the interface type of the generic repository
            var repositoryType = typeof(IRepository<,>);

            // Get the repository type which inherited from the generic repository
            var repositoies = new List<Type>();

            //foreach (var item in options.RepositoryAssemblies)
            //{
            //    // Get all class which inherited IRepository
            //    //
            //    var types = Assembly.Load(item).GetTypes()
            //        .Where(i => i.BaseType != null && i.BaseType.Name == repositoryType.Name);

            //    if (types.Count() == 0 || !types.Any())
            //        throw new ArgumentNullException(nameof(options.RepositoryAssemblies));

            //    repositoies.AddRange(types);
            //}

            //services.AddScoped(repositoryType, repositoies);

            return services;
        }
    }
}