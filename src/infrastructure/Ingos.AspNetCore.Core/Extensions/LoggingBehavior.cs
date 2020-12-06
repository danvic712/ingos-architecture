//-----------------------------------------------------------------------
// <copyright file= "LoggingBehavior.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/29 17:47:13
// Modified by:
// Description: Log MediatR request behavior info
//-----------------------------------------------------------------------

using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ingos.AspNetCore.Core.Extensions
{
    /// <summary>
    ///     Log behavior
    /// </summary>
    /// <typeparam name="TRequest">Request</typeparam>
    /// <typeparam name="TResponse">Response</typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        /// <summary>
        ///     Http instance
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        ///     Log instance
        /// </summary>
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="logger">Log instance</param>
        /// <param name="httpContextAccessor">Http instance</param>
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        ///     Handle
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <param name="next">The next request</param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var traceId = _httpContextAccessor.HttpContext.TraceIdentifier;

            var command = typeof(TRequest).Name;

            _logger.LogInformation(
                $"TraceId={traceId},Command={command},Request={JsonSerializer.Serialize(request)}");

            var response = await next();

            _logger.LogInformation(
                $"TraceId={traceId},Command={command},Response={JsonSerializer.Serialize(response)}");

            return response;
        }
    }
}