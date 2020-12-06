//-----------------------------------------------------------------------
// <copyright file= "ApiResponse.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/21 22:31:44
// Modified by:
// Description: Standard api response object
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Ingos.AspNetCore.Core.Responses
{
    public class ApiResponse<T> where T : class
    {
        #region Properties

        /// <summary>
        ///     Request trace id
        /// </summary>
        public string TraceId { get; set; }

        /// <summary>
        ///     Request status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        ///     Response data
        /// </summary>
        public T Resource { get; set; } = default;

        /// <summary>
        ///     Error information
        /// </summary>
        public IList<ApiResponseErrorMessage> Error { get; set; } = new List<ApiResponseErrorMessage>();

        #endregion Properties
    }
}