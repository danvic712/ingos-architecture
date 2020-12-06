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
    public class ApiPaginationResponse<T> : ApiResponse<PaginationResource<T>>
    {
    }

    public class PaginationResource<T>
    {
        #region Properties

        /// <summary>
        ///     Total data
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        ///     Current page
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        ///     Next page
        /// </summary>
        public int Next { get; set; }

        /// <summary>
        ///     Paging data
        /// </summary>
        public IList<T> Data { get; set; } = new List<T>();

        #endregion Properties
    }
}