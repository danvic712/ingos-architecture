//-----------------------------------------------------------------------
// <copyright file= "PageList.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/5/2 13:17:24
// Modified by:
// Description: Paginate resources
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Ingos.Infrastructure.Core.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        #region Attributes

        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Previous page
        /// </summary>
        public int PreviousPage { get; set; }

        /// <summary>
        /// Next page
        /// </summary>
        public int NextPage { get; set; }

        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; set; }

        #endregion Attributes
    }
}