//-----------------------------------------------------------------------
// <copyright file= "IngosServiceOptions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/22 11:06:04
// Modified by:
// Description: Ingos default service config options
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace Ingos.AspNetCore.Core.DependencyInjection
{
    public class IngosServiceOptions
    {
        #region Properties

        /// <summary>
        ///     The options for Swagger
        /// </summary>
        public SwaggerOptions SwaggerOptions { get; set; } = new SwaggerOptions();

        /// <summary>
        ///     The options for AutoMapper
        /// </summary>
        public AutoMapperOptions AutoMapperOptions { get; set; } = new AutoMapperOptions();

        /// <summary>
        ///     The options of MediatR
        /// </summary>
        public MediatROptions MediatROptions { get; set; } = new MediatROptions();

        #endregion Properties
    }

    /// <summary>
    ///     Swagger config options
    /// </summary>
    public class SwaggerOptions
    {
        /// <summary>
        ///     Open Api info
        /// </summary>
        public OpenApiInfo OpenApiInfo { get; set; } = new OpenApiInfo
        {
            Title = "Ingos Business Backend API"
        };

        /// <summary>
        ///     The paths of api comment's xml
        /// </summary>
        public IList<string> ApiCommentPaths { get; set; }
    }

    /// <summary>
    ///     AutoMapper config options
    /// </summary>
    public class AutoMapperOptions
    {
        /// <summary>
        ///     The assemblies contains mapper profiles
        /// </summary>
        public IList<string> Profiles { get; set; }
    }

    /// <summary>
    ///     MediatR config options
    /// </summary>
    public class MediatROptions
    {
        /// <summary>
        ///     The assemblies contains mediators
        /// </summary>
        public IList<string> Mediators { get; set; }

        /// <summary>
        ///     Whether to logger behavior info
        /// </summary>
        public bool LogBehavior { get; set; } = true;
    }
}