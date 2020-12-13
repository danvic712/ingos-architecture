//-----------------------------------------------------------------------
// <copyright file= "TestDbContext.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/12/13 14:27:16
// Modified by:
// Description: Test database context
//-----------------------------------------------------------------------

using Ingos.EntityFrameworkCore.Repository.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ingos.EntityFrameworkCore.Repository.Tests
{
    public class TestDbContext : IngosDbContext
    {
        public TestDbContext(DbContextOptions<IngosDbContext> options) : base(options)
        {
        }

        public DbSet<TestEntity> TestEntities { get; set; }
    }
}