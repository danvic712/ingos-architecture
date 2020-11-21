//-----------------------------------------------------------------------
// <copyright file= "ResourceOperation.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/21 22:37:07
// Modified by:
// Description: Standard resource operation return object
//-----------------------------------------------------------------------

using System;

namespace Ingos.AspNetCore.Core.Responses
{
    public class ResourceOperation<TPrimaryKey>
    {
        #region Properties

        /// <summary>
        ///     Operational data primary key
        /// </summary>
        public TPrimaryKey Id { get; set; }

        /// <summary>
        ///     Details message
        /// </summary>
        public string Message { get; set; }

        #endregion Properties
    }

    /// <summary>
    ///     The base response operation of primary key type guid
    /// </summary>
    public class ResourceOperation : ResourceOperation<Guid>
    {
    }
}