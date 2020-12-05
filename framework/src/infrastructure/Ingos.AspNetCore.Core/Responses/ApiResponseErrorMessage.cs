//-----------------------------------------------------------------------
// <copyright file= "ApiResponseErrorMessage.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/21 22:27:33
// Modified by:
// Description: Api error message data object
//-----------------------------------------------------------------------

namespace Ingos.AspNetCore.Core.Responses
{
    public class ApiResponseErrorMessage
    {
        #region Properties

        /// <summary>
        ///     Error Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Detailed error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Error stack information
        /// </summary>
        public string Stack { get; set; }

        #endregion Properties
    }
}