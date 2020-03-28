//-----------------------------------------------------------------------
// <copyright file= "ConfigurationOptions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/3/8 20:14:47
// Modified by:
// Description: Ingos custom AutoMapper config options
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace Ingos.Infrastructure.Core.AutoMapper
{
    public class IngosAutoMapperConfigurationOptions
    {
        #region Attributes

        /// <summary>
        /// The assemblies which contains mapper rules
        /// </summary>
        public IEnumerable<string> Assemblies { get; set; }

        #endregion Attributes
    }
}