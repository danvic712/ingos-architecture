//-----------------------------------------------------------------------
// <copyright file= "IDomainEvent.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/3/28 20:05:28
// Modified by:
// Description: Domain event interface
//-----------------------------------------------------------------------
using MediatR;

namespace Ingos.Domain.Abstractions.Contracts
{
    public interface IDomainEvent : INotification
    { }
}