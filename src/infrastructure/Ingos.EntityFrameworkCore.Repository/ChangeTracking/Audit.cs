//-----------------------------------------------------------------------
// <copyright file= "Audit.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/12/13 15:01:47
// Modified by:
// Description: Table change audit info
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingos.EntityFrameworkCore.Repository.ChangeTracking
{
    public class Audit
    {
        /// <summary>
        ///     Audit record primary key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        ///     Http
        /// </summary>
        [MaxLength(50)]
        public string TraceId { get; set; }

        /// <summary>
        ///     Table name
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string TableName { get; set; }

        /// <summary>
        ///     Changed record's primary key
        /// </summary>
        [MaxLength(50)]
        public string KeyValue { get; set; }

        /// <summary>
        ///     Data operation type
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string DataOperationType { get; set; }

        /// <summary>
        ///     Original values
        /// </summary>
        public string OriginalValue { get; set; }

        /// <summary>
        ///     Changed values
        /// </summary>
        public string ChangedValue { get; set; }

        /// <summary>
        ///     Creation time
        /// </summary>
        [Required]
        public DateTimeOffset DateTime { get; set; }
    }
}