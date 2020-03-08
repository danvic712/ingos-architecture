//-----------------------------------------------------------------------
// <copyright file= "AutoMapperServiceCollectionExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/3/8 20:19:01
// Modified by:
// Description: Ingos AutoMapper custom service injection extension method
//-----------------------------------------------------------------------
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ingos.Infrastructure.Core.AutoMapper.DependencyInjection
{
    public static class AutoMapperServiceCollectionExtensions
    {
        /// <summary>
        /// Inject AutoMapper into IServiceCollection
        /// </summary>
        /// <param name="services">The services that need to be injected into the container <see cref="IServiceCollection"/></param>
        /// <param name="setupAction">The instance of Ingos AutoMapper config options</param>
        /// <returns></returns>
        public static IServiceCollection AddIngosAutoMapperProfiles(this IServiceCollection services,
            Action<IngosAutoMapperConfigurationOptions> setupAction)
        {
            if (setupAction == null)
                throw new ArgumentNullException(nameof(setupAction));

            // Get config options
            //
            var options = new IngosAutoMapperConfigurationOptions();
            setupAction?.Invoke(options);

            return AddAutoMapperService(services, options);
        }

        /// <summary>
        /// Add AutoMapper
        /// </summary>
        /// <param name="services">The instance of service collections</param>
        /// <param name="options">The instance of Ingos AutoMapper config options</param>
        /// <returns></returns>
        private static IServiceCollection AddAutoMapperService(IServiceCollection services, IngosAutoMapperConfigurationOptions options)
        {
            var profiles = new List<Type>();

            // The base mapping profile class's type
            var parentType = typeof(Profile);

            foreach (var item in options.Assemblies)
            {
                // Get all class which inheritance Profile class
                //
                var types = Assembly.Load(item).GetTypes()
                    .Where(i => i.BaseType != null && i.BaseType.Name == parentType.Name);

                if (types.Count() == 0 || !types.Any())
                    throw new ArgumentNullException(nameof(options.Assemblies));

                profiles.AddRange(types);
            }

            // Add mapping rules
            if (profiles.Count() != 0 || profiles.Any())
                services.AddAutoMapper(profiles.ToArray());

            return services;
        }
    }
}