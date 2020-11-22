//-----------------------------------------------------------------------
// <copyright file= "ServiceCollectionExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/21 22:25:15
// Modified by:
// Description:  Inject service into services collection
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Text.Json;
using Ingos.AspNetCore.Core.DependencyInjection;
using Ingos.AspNetCore.Core.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add default services into services collection
        /// </summary>
        /// <param name="services">Services container</param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddIngosServices(this IServiceCollection services,
            Action<IngosServicesOptions> setupAction)
        {
            if (setupAction == null)
                throw new ArgumentNullException(nameof(setupAction));

            var options = new IngosServicesOptions();
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
            IngosServicesOptions options)
        {
            // Use lowercase routing and lowercase query string
            services.AddRouting(routeOptions =>
            {
                routeOptions.LowercaseUrls = true;
                routeOptions.LowercaseQueryStrings = true;
            });

            services.AddHttpContextAccessor();

            services.AddApiVersionService()
                .AddCustomInvalidModelState();

            services.AddHealthChecks();

            return services;
        }

        /// <summary>
        ///     Add api version service into services collection
        /// </summary>
        /// <param name="services">Services container</param>
        /// <returns> </returns>
        public static IServiceCollection AddApiVersionService(this IServiceCollection services)
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

        /// <summary>
        ///     Add custom model state invalid return info
        /// </summary>
        /// <param name="services">Services container</param>
        /// <returns></returns>
        public static IServiceCollection AddCustomInvalidModelState(this IServiceCollection services)
        {
            // Get the service provider
            var provider = services.BuildServiceProvider();

            // Use service location get log and http instance
            //
            var logger = provider.GetRequiredService<ILogger<StartupBase>>();
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    // Get fields that failed verification
                    //
                    var errors = actionContext.ModelState.Where(e =>
                        e.Value.Errors.Count > 0).Select(e => new ApiResponseErrorMessage
                    {
                        Title = "Request parameters verification failed",
                        Message = e.Value.Errors.FirstOrDefault()?.ErrorMessage
                    }).ToList();

                    var result = new ApiResponse<object>
                    {
                        TraceId = httpContextAccessor.HttpContext.TraceIdentifier,
                        Status = false,
                        Error = errors
                    };

                    logger.LogError($"Request parameters verification failed: {JsonSerializer.Serialize(result)}");

                    return new BadRequestObjectResult(result);
                };
            });

            return services;
        }
    }
}