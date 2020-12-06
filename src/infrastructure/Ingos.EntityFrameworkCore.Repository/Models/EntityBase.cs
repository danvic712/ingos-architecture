//-----------------------------------------------------------------------
// <copyright file= "EntityBase.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/7/6 19:40:43
// Modified by:
// Description: Abstract base entity class
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Ingos.EntityFrameworkCore.Repository.Models
{
    public abstract class EntityBase<TPrimaryKey>
    {
        #region Properties

        /// <summary>
        ///     Primary key
        /// </summary>
        [Key]
        public TPrimaryKey Id { get; set; }

        #endregion Properties
    }

    /// <summary>
    ///     The base entity of primary key type guid
    /// </summary>
    public abstract class EntityBase : EntityBase<Guid>
    {
    }
}