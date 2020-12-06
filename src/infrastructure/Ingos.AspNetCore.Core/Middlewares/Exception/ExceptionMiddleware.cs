//-----------------------------------------------------------------------
// <copyright file= "ExceptionMiddleware.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/21 22:34:49
// Modified by:
// Description: Exception handle middleware
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Ingos.AspNetCore.Core.Responses;
using Ingos.Infrastructure.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Ingos.AspNetCore.Core.Middlewares.Exception
{
    /// <summary>
    ///     Exception handle middleware
    /// </summary>
    public class ExceptionMiddleware
    {
        #region Initialize

        /// <summary>
        ///     Http request delegate
        /// </summary>
        private readonly RequestDelegate _request;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="request">Http request delegate</param>
        public ExceptionMiddleware(RequestDelegate request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }

        #endregion Initialize

        #region Methods

        /// <summary>
        ///     Inject middleware into http context
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        ///     Handle error
        /// </summary>
        /// <param name="httpContext">Http context</param>
        /// <param name="exception">Error message</param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext httpContext, System.Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            // If this error is the system business logic error then return 200, else return 500
            httpContext.Response.StatusCode = exception is BusinessLogicException
                ? StatusCodes.Status200OK
                : StatusCodes.Status500InternalServerError;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true
            };

            // Error info
            var result = JsonSerializer.Serialize(new ApiResponse<object>
            {
                TraceId = httpContext.TraceIdentifier,
                Status = false,
                Error = new List<ApiResponseErrorMessage>
                {
                    new ApiResponseErrorMessage
                    {
                        Code = "InternalError",
                        Message = exception.InnerException == null
                            ? exception.Message
                            : exception.InnerException.Message,
                        Stack = exception.StackTrace?.Trim()
                    }
                }
            }, options);

            await httpContext.Response.WriteAsync(result);
        }

        #endregion Methods
    }
}