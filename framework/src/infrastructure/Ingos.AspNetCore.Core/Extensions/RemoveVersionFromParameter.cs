//-----------------------------------------------------------------------
// <copyright file= "RemoveVersionFromParameter.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/29 17:02:22
// Modified by:
// Description: Remove api version param from swagger doc
//-----------------------------------------------------------------------

using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ingos.AspNetCore.Core.Extensions
{
    /// <summary>
    ///     Remove swagger doc's api version parameter
    /// </summary>
    public class RemoveVersionFromParameter : IOperationFilter
    {
        /// <summary>
        ///     Apply the filter rule
        /// </summary>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter =
                operation.Parameters.FirstOrDefault(p => p.Name.Equals("version") || p.Name.Equals("api-version"));
            operation.Parameters.Remove(versionParameter);
        }
    }
}