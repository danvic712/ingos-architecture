//-----------------------------------------------------------------------
// <copyright file= "RequestStorageMiddleware.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/21 22:34:49
// Modified by:
// Description: Log http request and response info
//-----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ingos.AspNetCore.Core.Middlewares.RequestStorage
{
    /// <summary>
    ///     Log http request and response info
    /// </summary>
    public class RequestStorageMiddleware
    {
        #region Initialize

        /// <summary>
        ///     Log instance
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     Http request delegate
        /// </summary>
        private readonly RequestDelegate _request;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="logger">Log instance</param>
        /// <param name="request">Http request delegate</param>
        public RequestStorageMiddleware(ILogger<RequestStorageMiddleware> logger, RequestDelegate request)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            // Get request info
            var request = await FormatRequestAsync(context.Request);

            // Get response body info
            //
            var originalBodyStream = context.Response.Body;
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Time to record response cost
            //
            var sp = new Stopwatch();
            sp.Start();
            await _request(context);
            sp.Stop();

            // Log response time
            //
            var response = await FormatResponseAsync(context.Response);
            _logger.LogInformation(
                $"TraceId={context.TraceIdentifier},Scheme={context.Request.Scheme},Host={context.Request.Host},Path={context.Request.Path}" +
                $",QueryString={context.Request.QueryString},Body={request},Cost={sp.ElapsedMilliseconds}ms,Response={response}");

            await responseBody.CopyToAsync(originalBodyStream);
        }

        /// <summary>
        ///     Format request info
        /// </summary>
        /// <param name="request">HTTP request info</param>
        /// <returns></returns>
        private static async Task<string> FormatRequestAsync(HttpRequest request)
        {
            request.EnableBuffering();

            request.Body.Seek(0, SeekOrigin.Begin);

            var text = await new StreamReader(request.Body).ReadToEndAsync();

            request.Body.Seek(0, SeekOrigin.Begin);

            return text.Trim().Replace("\r", "").Replace("\n", "");
        }

        /// <summary>
        ///     Format response info
        /// </summary>
        /// <param name="response">HTTP response info</param>
        /// <returns></returns>
        private static async Task<string> FormatResponseAsync(HttpResponse response)
        {
            if (response.HasStarted)
                return string.Empty;

            response.Body.Seek(0, SeekOrigin.Begin);

            var text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return text.Trim().Replace("\r", "").Replace("\n", "");
        }

        #endregion Methods
    }
}