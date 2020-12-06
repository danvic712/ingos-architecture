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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using AutoMapper;
using Ingos.AspNetCore.Core.DependencyInjection;
using Ingos.AspNetCore.Core.Extensions;
using Ingos.AspNetCore.Core.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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
            Action<IngosServiceOptions> setupAction = null)
        {
            var options = new IngosServiceOptions();
            setupAction?.Invoke(options);

            return InjectServiceCore(services, options);
        }

        #region Application Services

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
        ///     Add AutoMapper into services collection
        /// </summary>
        /// <param name="services">Services container</param>
        /// <param name="options">The inject AutoMapper config options</param>
        /// <returns> </returns>
        public static IServiceCollection AddAutoMapperService(this IServiceCollection services,
            AutoMapperOptions options)
        {
            // Get assemblies which contains mapper profiles
            //
            var rules = options.Profiles.Select(Assembly.Load)
                .Where(profiles => profiles != null).ToList();
            if (!rules.Any())
                return services;

            // Inject service into container
            services.AddAutoMapper(rules);

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
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var jsonSerializerOptions = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        IgnoreNullValues = true,
                        WriteIndented = true
                    };

                    // Get fields that failed verification
                    //
                    var errors = actionContext.ModelState.Where(e =>
                        e.Value.Errors.Count > 0).Select(e => new ApiResponseErrorMessage
                        {
                            Code = "InvalidArgument",
                            Message = e.Value.Errors.FirstOrDefault()?.ErrorMessage
                        }).ToList();

                    // Error info
                    var result = JsonSerializer.Serialize(new ApiResponse<object>
                    {
                        TraceId = httpContext.TraceIdentifier,
                        Status = false,
                        Error = errors
                    }, jsonSerializerOptions);

                    return new BadRequestObjectResult(result);
                };
            });

            return services;
        }

        /// <summary>
        ///     Add MediatR into services collection
        /// </summary>
        /// <param name="services">Services container</param>
        /// <param name="options">The inject MediatR config options</param>
        /// <returns> </returns>
        public static IServiceCollection AddMediatRService(this IServiceCollection services,
            MediatROptions options)
        {
            // Get assemblies which contains rules
            //
            var rules = options.Mediators.Select(Assembly.Load).ToArray();
            if (!rules.Any())
                return services;

            // Inject service into container
            services.AddMediatR(rules);

            if (options.LogBehavior)
                services.AddTransient(typeof(IPipelineBehavior<,>),
                    typeof(LoggingBehavior<,>));

            return services;
        }

        /// <summary>
        ///     Add Swagger into services collection
        /// </summary>
        /// <param name="services">Services container</param>
        /// <param name="options">The inject swagger config options</param>
        /// <returns> </returns>
        public static IServiceCollection AddSwaggerService(this IServiceCollection services,
            SwaggerOptions options)
        {
            // Config swagger doc info
            services.AddSwaggerGen(s =>
            {
                // Generate api doc by api version info
                //
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.OpenApiInfo.Version = description.ApiVersion.ToString();
                    s.SwaggerDoc(description.GroupName, options.OpenApiInfo);
                }

                // Show api version in the url which swagger doc generated
                s.DocInclusionPredicate((version, apiDescription) =>
                {
                    // Just show this version's api
                    if (!version.Equals(apiDescription.GroupName))
                        return false;

                    var values = apiDescription.RelativePath
                        .Split('/')
                        .Select(v => v.Replace("v{version}", apiDescription.GroupName));
                    apiDescription.RelativePath = string.Join("/", values);
                    return true;
                });

                // Let params use the camel naming method
                s.DescribeAllParametersInCamelCase();

                // Remove version param must input in swagger doc
                s.OperationFilter<RemoveVersionFromParameter>();

                // Get project's api description file
                //
                GetApiDocPaths(options.ApiCommentPaths, Path.GetDirectoryName(AppContext.BaseDirectory))
                    .ForEach(x => s.IncludeXmlComments(x, true));
            });

            return services;
        }

        #endregion Application Services

        #region Methods

        /// <summary>
        ///     Get the api description doc path
        /// </summary>
        /// <param name="paths">The xml file path</param>
        /// <param name="basePath">The site's base running files path</param>
        /// <returns></returns>
        private static List<string> GetApiDocPaths(IEnumerable<string> paths, string basePath)
        {
            var files = from path in paths
                        let xml = Path.Combine(basePath, path)
                        select xml;

            return files.ToList();
        }

        /// <summary>
        ///     Core method of inject service
        /// </summary>
        /// <param name="services">The services collection</param>
        /// <param name="options">The inject service's config options</param>
        /// <returns></returns>
        private static IServiceCollection InjectServiceCore(IServiceCollection services,
            IngosServiceOptions options)
        {
            // Use lowercase routing and lowercase query string
            services.AddRouting(routeOptions =>
            {
                routeOptions.LowercaseUrls = true;
                routeOptions.LowercaseQueryStrings = true;
            });

            services.AddApiVersionService()
                .AddHttpContextAccessor()
                .AddCustomInvalidModelState()
                .AddAutoMapperService(options.AutoMapperOptions)
                .AddMediatRService(options.MediatROptions)
                .AddSwaggerService(options.SwaggerOptions);

            services.AddHealthChecks();

            return services;
        }

        #endregion Methods
    }
}