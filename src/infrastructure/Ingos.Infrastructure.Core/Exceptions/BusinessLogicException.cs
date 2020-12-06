//-----------------------------------------------------------------------
// <copyright file= "BusinessLogicException.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/21 21:08:19
// Modified by:
// Description: System business logic error
//-----------------------------------------------------------------------

using System;

namespace Ingos.Infrastructure.Core.Exceptions
{
    [Serializable]
    public class BusinessLogicException : ApplicationException
    {
        /// <summary>
        ///     ctor
        /// </summary>
        public BusinessLogicException()
        {
        }

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="message">Error message</param>
        public BusinessLogicException(string message) : base(message)
        {
        }

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="exception">Internal error</param>
        public BusinessLogicException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}