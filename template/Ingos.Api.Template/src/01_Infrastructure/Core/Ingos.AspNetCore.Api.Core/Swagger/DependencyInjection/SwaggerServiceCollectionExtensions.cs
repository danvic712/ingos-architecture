//-----------------------------------------------------------------------
// <copyright file= "SwaggerServiceCollectionExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/3/8 20:33:50
// Modified by:
// Description: Ingos Swagger custom service injection extension method
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ingos.Api.Core.Swagger.DependencyInjection
{
    public static class SwaggerServiceCollectionExtensions
    {
        /// <summary>
        /// Inject Swagger into IServiceCollection
        /// </summary>
        /// <param name="services">The services that need to be injected into the container <see cref="IServiceCollection"/></param>
        /// <param name="setupAction">The instance of Ingos Swagger config options <see cref="IngosConfigurationOptions"/></param>
        public static IServiceCollection AddIngosApplicationSwagger(this IServiceCollection services,
            Action<IngosConfigurationOptions> setupAction)
        {
            if (setupAction == null)
                throw new ArgumentNullException(nameof(setupAction));

            // Get swagger config options
            //
            var options = new IngosConfigurationOptions();
            setupAction?.Invoke(options);

            return AddSwaggerService(services, options);
        }

        /// <summary>
        /// Add Swagger
        /// </summary>
        /// <param name="services">The collection of services</param>
        /// <param name="options">The swagger config options</param>
        /// <returns></returns>
        private static IServiceCollection AddSwaggerService(IServiceCollection services, IngosConfigurationOptions options)
        {
            // Config swagger doc info
            services.AddSwaggerGen(s =>
            {
                // Generate api doc by api version info
                //
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    s.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Contact = new OpenApiContact
                        {
                            Name = options.Name,
                            Email = options.Email,
                            Url = options.Url,
                        },
                        Description = options.Description,
                        Title = options.Title,
                        License = options.License,
                        Version = description.ApiVersion.ToString()
                    });
                }

                // Show api version in the url which swagger doc generated
                s.DocInclusionPredicate((version, apiDescription) =>
                {
                    // Just show this version's api
                    if (!version.Equals(apiDescription.GroupName))
                        return false;

                    var values = apiDescription.RelativePath
                        .Split('/')
                        .Select(v => v.Replace("v{version}", apiDescription.GroupName)); apiDescription.RelativePath = string.Join("/", values);
                    return true;
                });

                // Let params use the camel naming method
                s.DescribeAllParametersInCamelCase();

                // Remove version param must input in swagger doc
                s.OperationFilter<RemoveVersionFromParameter>();

                // Get project's api description file
                //
                var paths = options.Paths ?? Array.Empty<string>();
                if (!paths.Any())
                    return;

                GetApiDocPaths(paths, Path.GetDirectoryName(AppContext.BaseDirectory))
                    .ForEach(x => s.IncludeXmlComments(x, true));
            });

            return services;
        }

        /// <summary>
        /// Get the api description doc path
        /// </summary>
        /// <param name="paths">The xml file path</param>
        /// <param name="basePath">The site's base running files path</param>
        /// <returns></returns>
        private static List<string> GetApiDocPaths(IEnumerable<string> paths, string basePath)
        {
            var xmls = from path in paths
                       let xml = Path.Combine(basePath, path)
                       select xml;

            return xmls.ToList();
        }
    }
}