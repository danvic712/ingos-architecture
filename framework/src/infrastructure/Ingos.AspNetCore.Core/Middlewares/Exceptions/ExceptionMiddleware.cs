using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Ingos.AspNetCore.Core.Responses;
using Ingos.Infrastructure.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ingos.AspNetCore.Core.Middlewares.Exceptions
{
    /// <summary>
    ///     Exception middleware handle
    /// </summary>
    public class ExceptionMiddleware
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
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate request)
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
            try
            {
                await _request(context);
            }
            catch (Exception ex)
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
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            // If this error is the system business logic error then return 200, else return 500
            httpContext.Response.StatusCode = exception is BusinessLogicException
                ? StatusCodes.Status200OK
                : StatusCodes.Status500InternalServerError;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            // Error info
            var result = JsonSerializer.Serialize(new ApiResponse<object>
            {
                TraceId = httpContext.TraceIdentifier,
                Status = false,
                Error = new List<ApiResponseErrorMessage>
                {
                    new ApiResponseErrorMessage
                    {
                        // Todo：not implement this define
                        ErrorCode = "",
                        Message = exception.InnerException == null
                            ? exception.Message
                            : exception.InnerException.Message
                    }
                }
            }, options);

            _logger.LogError($"API request failed：{result}");

            await httpContext.Response.WriteAsync(result);
        }

        #endregion Methods
    }
}