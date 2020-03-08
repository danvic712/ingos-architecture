//-----------------------------------------------------------------------
// <copyright file= "IngosMediatRDescriptionOptions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/3/8 20:07:27
// Modified by:
// Description: Ingos custom MediatR config options
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Ingos.Infrastructure.Core.MediatR
{
    public class IngosMediatRDescriptionOptions
    {
        #region Attributes

        /// <summary>
        /// Application startup‘s class type
        /// </summary>
        public Type StartupClassType { get; set; }

        /// <summary>
        /// The assemblies which contains mediator classes
        /// </summary>
        public IEnumerable<string> Assemblies { get; set; }

        #endregion Attributes
    }
}