//-----------------------------------------------------------------------
// <copyright file= "AuditEntityBase.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/11/15 10:28:48
// Modified by:
// Description: Abstract base audit entity class
//-----------------------------------------------------------------------

using System;

namespace Ingos.EntityFrameworkCore.Repository.Models
{
    public abstract class AuditEntityBase<TPrimaryKey> : EntityBase<TPrimaryKey>
    {
        #region Properties

        /// <summary>
        ///     Creator's name
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        ///     Creation time
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     Modifier's name
        /// </summary>
        public string Modifier { get; set; }

        /// <summary>
        ///     Modify time
        /// </summary>
        public DateTime ModifyTime { get; set; }

        #endregion Properties
    }

    /// <summary>
    ///     The base audit entity of primary key type guid
    /// </summary>
    public class AuditEntityBase : AuditEntityBase<Guid>
    {
    }
}