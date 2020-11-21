//-----------------------------------------------------------------------
// <copyright file= "ApiPaginationResponse.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/21 22:34:49
// Modified by:
// Description: Standard api pagination response object
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Ingos.AspNetCore.Core.Responses
{
    public class ApiPaginationResponse<T> where T : class
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
        ///     Current page
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        ///     Next page
        /// </summary>
        public int Next { get; set; }

        /// <summary>
        ///     Total data
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        ///     Response data
        /// </summary>
        public IList<T> Data { get; set; }

        /// <summary>
        ///     Error information
        /// </summary>
        public IList<ApiResponseErrorMessage> Error { get; set; }

        #endregion Properties
    }
}