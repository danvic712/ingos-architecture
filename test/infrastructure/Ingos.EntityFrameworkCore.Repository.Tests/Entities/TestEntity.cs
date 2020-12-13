//-----------------------------------------------------------------------
// <copyright file= "TestEntity.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/12/13 14:38:12
// Modified by:
// Description:
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Ingos.EntityFrameworkCore.Repository.Tests.Entities
{
    public class TestEntity
    {
        [Key] public long Id { get; set; }

        public string Name { get; set; }

        public bool Gender { get; set; }

        public DateTime CreateTime { get; set; }
    }
}