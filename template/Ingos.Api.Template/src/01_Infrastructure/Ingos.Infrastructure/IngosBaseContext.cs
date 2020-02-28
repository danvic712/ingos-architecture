//-----------------------------------------------------------------------
// <copyright file= "IngosBaseContext.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 21:45:48
// Modified by:
// Description:
//-----------------------------------------------------------------------
using Ingos.Infrastructure.Core.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ingos.Infrastructure
{
    public class IngosBaseContext : IngosApplicationBaseContext
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"> </param>
        /// <param name="mediator"> </param>
        public IngosBaseContext(DbContextOptions options, IMediator mediator)
            : base(options, mediator)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}