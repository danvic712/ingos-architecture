//-----------------------------------------------------------------------
// <copyright file= "EntityBase.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 22:25:19
// Modified by:
// Description: Generic base domain object class
//-----------------------------------------------------------------------
using Ingos.Domain.Abstractions.Contracts;
using System.Collections.Generic;

namespace Ingos.Domain.Abstractions
{
    /// <summary>
    /// Abstract domain object base class
    /// </summary>
    /// <typeparam name="TPrimaryKey">Primary key</typeparam>
    public abstract class EntityBase<TPrimaryKey>
    {
        #region Domain Attributes

        /// <summary>
        /// Primary key
        /// </summary>
        public TPrimaryKey Id { get; protected set; }

        #endregion Domain Attributes

        #region Domain Events

        /// <summary>
        /// Get domain events
        /// </summary>
        private List<IDomainEvent> _domainEvents;

        /// <summary>
        /// Domain events collection
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// Add domain event
        /// </summary>
        /// <param name="domainEvent">Domain event</param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Clear all domain event
        /// </summary>
        public void ClearDomainEvents() => _domainEvents?.Clear();

        /// <summary>
        /// Remove domain event
        /// </summary>
        /// <param name="domainEvent">Domain events that need to be removed</param>
        public void RemoveDomainEvents(IDomainEvent domainEvent) => _domainEvents?.Remove(domainEvent);

        #endregion Domain Events

        #region Domain methods

        /// <summary>
        /// Determine the two classes are not equal
        /// </summary>
        /// <param name="a">Class a</param>
        /// <param name="b">Class b</param>
        /// <returns></returns>
        public static bool operator !=(EntityBase<TPrimaryKey> a, EntityBase<TPrimaryKey> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Determine the two classes are equal
        /// </summary>
        /// <param name="a">Class a</param>
        /// <param name="b">Class b</param>
        /// <returns></returns>
        public static bool operator ==(EntityBase<TPrimaryKey> a, EntityBase<TPrimaryKey> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Check whether the obj is null
            if (!(obj is EntityBase<TPrimaryKey> compareTo))
                return false;

            // Check whether the two obj is the same or whether the primary key is the same
            return ReferenceEquals(this, compareTo) || Id.Equals(compareTo.Id);
        }

        /// <summary>
        /// Get the object's hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<TPrimaryKey>.Default.GetHashCode(Id);
        }

        /// <summary>
        /// Rewrite the ToString method to return the object info
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }

        #endregion Domain methods
    }

    /// <summary>
    /// The base entity of type long
    /// </summary>
    public abstract class EntityBase : EntityBase<long>
    { }
}