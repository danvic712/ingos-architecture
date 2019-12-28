//-----------------------------------------------------------------------
// <copyright file= "IngosAutoMapperOptions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2019/12/28 15:35:21
// Modified by:
// Description: Ingos custom config options
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Ingos.AutoMapper
{
    public class IngosAutoMapperOptions
    {
        #region Attributes

        /// <summary>
        /// The assemblies which contains mapper rules
        /// </summary>
        public ICollection<string> Assemblies { get; set; }

        #endregion Attributes
    }
}