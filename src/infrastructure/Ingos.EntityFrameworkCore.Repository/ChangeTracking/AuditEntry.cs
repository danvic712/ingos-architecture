//-----------------------------------------------------------------------
// <copyright file= "AuditEntry.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/12/13 15:34:03
// Modified by:
// Description: Audit entry info
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ingos.EntityFrameworkCore.Repository.ChangeTracking
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }

        public string TableName { get; set; }

        public Dictionary<string, object> KeyValue { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OriginalValue { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> ChangedValue { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    }
}