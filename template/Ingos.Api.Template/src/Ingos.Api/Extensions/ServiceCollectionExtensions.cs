//-----------------------------------------------------------------------
// <copyright file= "ServiceCollectionExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 21:02:46
// Modified by:
// Description: Services container injection extension method
//-----------------------------------------------------------------------
using Ingos.Api.Core.Swagger.DependencyInjection;
using Ingos.Infrastructure;
using Ingos.Infrastructure.Core.EntityFrameworkCore;
using Ingos.Infrastructure.Core.EntityFrameworkCore.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Ingos.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Add your method here which injection service into services container

        #region Api Services

        /// <summary>
        /// Add application support for api version
        /// </summary>
        /// <param name="services">Services container</param>
        /// <returns> </returns>
        public static IServiceCollection AddApplicationApiVersion(this IServiceCollection services)
        {
            // Add api version support
            services.AddApiVersioning(o =>
            {
                // return api version info in response header
                o.ReportApiVersions = true;

                // default api version
                o.DefaultApiVersion = new ApiVersion(1, 0);

                // when not specifying an api version, select the default version
                o.AssumeDefaultVersionWhenUnspecified = true;
            });

            // Config api version info
            services.AddVersionedApiExplorer(option =>
            {
                // Set api version group name format
                option.GroupNameFormat = "'v'VVV";

                // when not specifying an api version, select the default version
                option.AssumeDefaultVersionWhenUnspecified = true;
            });

            return services;
        }

        #endregion Api Services

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
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            return services;
        }

        #endregion Repository Services

        #region Swagger Services

        /// <summary>
        /// Add application support for swagger doc
        /// </summary>
        /// <param name="services">Services container</param>
        /// <returns> </returns>
        public static IServiceCollection AddApplicationSwagger(this IServiceCollection services)
        {
            services.AddIngosApplicationSwagger(options =>
            {
                options.Name = "Danvic Wang";
                options.Email = "danvic96@hotmail.com";
                options.Url = new Uri("https://yuiter.com");
                options.Description = "Ingos.API Template - A asp.net core back-end web api template.";
                options.Title = "Ingos.API Template";
                options.License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                };
                options.Paths = new List<string> { "Ingos.Api.xml" };
            });

            return services;
        }

        #endregion Swagger Services

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