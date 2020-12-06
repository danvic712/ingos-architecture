//-----------------------------------------------------------------------
// <copyright file= "RequestStorageMiddlewareExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/21 22:34:49
// Modified by:
// Description: Request storage middleware extension method
//-----------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;

namespace Ingos.AspNetCore.Core.Middlewares.RequestStorage
{
    /// <summary>
    ///     Request storage middleware extension method
    /// </summary>
    public static class RequestStorageMiddlewareExtensions
    {
        /// <summary>
        ///     Use request storage middleware
        /// </summary>
        /// <param name="builder">request pipeline. <see cref="IApplicationBuilder" /></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestStorage(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestStorageMiddleware>();
        }
    }
}