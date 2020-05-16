//-----------------------------------------------------------------------
// <copyright file= "ServiceLifetimeException.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/5/16 10:32:36
// Modified by:
// Description: Service inject exception
//-----------------------------------------------------------------------

using System;

namespace Ingos.Infrastructure.Core.ServiceLifetimes.Exceptions
{
    public class ServiceLifetimeException : Exception
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ServiceLifetimeException()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message">Error message</param>
        public ServiceLifetimeException(string message) : base(message)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="inner">Inner exception</param>
        public ServiceLifetimeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}