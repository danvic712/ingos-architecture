//-----------------------------------------------------------------------
// <copyright file= "IngosRepositoryConfigurationOptions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/3/28 20:56:48
// Modified by:
// Description: Ingos custom database repository config options
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Ingos.Infrastructure.Core.EntityFrameworkCore
{
    public class IngosRepositoryConfigurationOptions
    {
        #region Attributes

        /// <summary>
        /// The assemblies collection which contains repository
        /// </summary>
        public IEnumerable<string> RepositoryAssemblies { get; set; }

        #endregion Attributes
    }
}