//-----------------------------------------------------------------------
// <copyright file= "IDomainEventHandler.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/3/28 20:08:17
// Modified by:
// Description: Domain event handler interface
//-----------------------------------------------------------------------
using MediatR;

namespace Ingos.Domain.Abstractions.Contracts
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : IDomainEvent
    { }
}