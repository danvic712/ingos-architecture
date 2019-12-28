//-----------------------------------------------------------------------
// <copyright file= "IngosAutoMapperExtension.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2019/12/28 15:31:38
// Modified by:
// Description: Ingos custom AutoMapper dependency injection method
//-----------------------------------------------------------------------
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ingos.AutoMapper.Extensions
{
    public static class IngosAutoMapperExtension
    {
        /// <summary>
        /// Inject AutoMapper into IServiceCollection
        /// </summary>
        /// <param name="services">The instance of <see cref="IServiceCollection"/></param>
        /// <param name="setupAction">The instance of Ingos AutoMapper config options</param>
        /// <returns></returns>
        public static IServiceCollection AddIngosAutoMapperProfiles(this IServiceCollection services,
            Action<IngosAutoMapperOptions> setupAction)
        {
            // Get config options
            //
            var options = new IngosAutoMapperOptions();
            setupAction?.Invoke(options);

            return AddAutoMapperServices(services, options);
        }

        /// <summary>
        /// Add AutoMapper
        /// </summary>
        /// <param name="services">The instance of service collections</param>
        /// <param name="options">The instance of Ingos AutoMapper config options</param>
        /// <returns></returns>
        private static IServiceCollection AddAutoMapperServices(IServiceCollection services, IngosAutoMapperOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

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