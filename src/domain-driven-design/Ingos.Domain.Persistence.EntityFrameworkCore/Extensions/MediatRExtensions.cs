//-----------------------------------------------------------------------
// <copyright file= "MediatRExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/4/18 13:21:38
// Modified by:
// Description: MediatR extension method
//-----------------------------------------------------------------------
using System.Linq;
using System.Threading.Tasks;
using Ingos.Domain.Abstractions;
using MediatR;

namespace Ingos.Domain.Persistence.EntityFrameworkCore.Extensions
{
    public static class MediatRExtensions
    {
        /// <summary>
        /// Dispatch domain events
        /// </summary>
        /// <param name="mediator">A mediator instance</param>
        /// <param name="context">DB context object instance</param>
        /// <returns> </returns>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, BaseDbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<EntityBase>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var entityEntries = domainEntities.ToList();
            var domainEvents = entityEntries
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            entityEntries.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}