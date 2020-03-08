//-----------------------------------------------------------------------
// <copyright file= "ApplicationBuilderExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 21:11:24
// Modified by:
// Description: Application builder injection extension method
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ingos.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        // Add your method here which will be used in http request pipeline

        #region Swagger middlewares

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            // Staging and prodcution environments do not turn on swagger
            if (env.IsStaging() || env.IsProduction())
                return app;

            // Enable swagger doc
            //
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                // Default load the latest version
                foreach (var description in provider.ApiVersionDescriptions.Reverse())
                {
                    s.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        $"Ingos API {description.GroupName.ToUpperInvariant()}");
                }
            });

            // Redirect to swagger page
            app.Run(context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

            return app;
        }

        #endregion Swagger middlewares
    }
}