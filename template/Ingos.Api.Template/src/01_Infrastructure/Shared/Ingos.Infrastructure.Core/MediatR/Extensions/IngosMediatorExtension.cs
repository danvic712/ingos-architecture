//-----------------------------------------------------------------------
// <copyright file= "IngosMediatorExtension.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 22:41:30
// Modified by:
// Description:
//-----------------------------------------------------------------------
using Ingos.Domain.Abstractions;
using Ingos.Infrastructure.Core.EntityFrameworkCore;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace Ingos.Infrastructure.Core.MediatR.Extensions
{
    public static class IngosMediatorExtension
    {
        /// <summary>
        /// Dispatch domain events
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="context"></param>
        /// <returns> </returns>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, IngosApplicationBaseContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<EntityBase>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}