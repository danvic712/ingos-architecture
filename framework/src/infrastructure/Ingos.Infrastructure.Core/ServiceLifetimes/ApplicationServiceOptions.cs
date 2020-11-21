//-----------------------------------------------------------------------
// <copyright file= "ApplicationServiceOptions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/5/16 9:53:04
// Modified by:
// Description: Application service config options
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;

namespace Ingos.Infrastructure.Core.ServiceLifetimes
{
    public class ApplicationServiceOptions
    {
        /// <summary>
        ///     The assembly which contains services
        /// </summary>
        public IEnumerable<Assembly> Assembly { get; set; }
    }
}