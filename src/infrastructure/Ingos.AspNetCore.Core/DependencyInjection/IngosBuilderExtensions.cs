//-----------------------------------------------------------------------
// <copyright file= "ApplicationBuilderExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 21:11:24
// Modified by:
// Description: Middleware injection extension method
//-----------------------------------------------------------------------

using System.Linq;
using Ingos.AspNetCore.Core.Middlewares.Exception;
using Ingos.AspNetCore.Core.Middlewares.RequestStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Hosting;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Injection middleware into application builder
    /// </summary>
    public static class IngosBuilderExtensions
    {
        /// <summary>
        ///     Add default middleware into application builder
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Host environment</param>
        /// <param name="provider">Api version description</param>
        /// <returns></returns>
        public static IApplicationBuilder UseIngos(this IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            app.UseRequestStorage()
                .UseIngosExceptionHandler()
                .UseHealthChecks("/health")
                .UseSwaggerDocuments(env, provider);

            return app;
        }

        /// <summary>
        ///     Add swagger middleware into http request pipeline
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Host environment</param>
        /// <param name="provider">Api version description</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerDocuments(this IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            // Staging and production environments do not turn on swagger
            if (env.IsStaging() || env.IsProduction())
                return app;

            // Enable swagger doc
            //
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                // Default load the latest version
                foreach (var description in provider.ApiVersionDescriptions.Reverse())
                    s.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        $"Ingos Business API {description.GroupName.ToLowerInvariant()}");
            });

            return app;
        }
    }
}