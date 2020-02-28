//-----------------------------------------------------------------------
// <copyright file= "ServiceCollectionExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 21:02:46
// Modified by:
// Description: Services container injection extension method
//-----------------------------------------------------------------------
using Ingos.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingos.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Add your method here which injection service into services container

        #region Database Services

        /// <summary>
        /// Add application db context
        /// </summary>
        /// <param name="services"> Services container </param>
        /// <param name="options"> Db context config options </param>
        /// <returns> </returns>
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            return services.AddDbContext<IngosBaseContext>(options);
        }

        /// <summary>
        /// Add mysql database connection into services
        /// </summary>
        /// <param name="services"> Services container </param>
        /// <param name="connectionString"> Database connection string </param>
        /// <returns> </returns>
        public static IServiceCollection AddMySqlDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddApplicationDbContext(options =>
            {
                options.UseMySql(connectionString);
            });
        }

        #endregion Database Services

        #region Repository Services

        /// <summary>
        /// Add repository
        /// </summary>
        /// <param name="services"> Services container </param>
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            return services;
        }

        #endregion Repository Services

        #region Health check Services

        /// <summary>
        /// Add application health check support
        /// </summary>
        /// <param name="services"> Services container </param>
        public static void AddApplicationHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<IngosBaseContext>();
        }

        #endregion Health check Services
    }
}