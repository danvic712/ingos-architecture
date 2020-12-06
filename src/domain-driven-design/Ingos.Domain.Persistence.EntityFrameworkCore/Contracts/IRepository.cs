//-----------------------------------------------------------------------
// <copyright file= "IRepository.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/4/18 13:11:10
// Modified by:
// Description: Generic base repository interface
//-----------------------------------------------------------------------
using System.Threading;
using System.Threading.Tasks;
using Ingos.Domain.Abstractions;
using Ingos.Domain.Abstractions.Contracts;

namespace Ingos.Domain.Persistence.EntityFrameworkCore.Contracts
{
    public interface IRepository<TEntity, in TPrimaryKey> where TEntity : EntityBase<TPrimaryKey>, IAggregateRoot
    {
        /// <summary>
        /// Unit of work object
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Delete aggregate root entity by primary key
        /// </summary>
        /// <param name="id">The primary key of this aggregate root entity</param>
        /// <returns> </returns>
        bool Delete(TPrimaryKey id);

        /// <summary>
        /// Delete aggregate root entity by primary key
        /// </summary>
        /// <param name="id">The primary key of this aggregate root entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<bool> DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get aggregate root entity by primary key
        /// </summary>
        /// <param name="id">The primary key of this aggregate root entity</param>
        /// <returns> </returns>
        TEntity GetEntityById(TPrimaryKey id);

        /// <summary>
        /// Get aggregate root entity by primary key
        /// </summary>
        /// <param name="id">The primary key of this aggregate root entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<TEntity> GetEntityByIdAsync(TPrimaryKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Insert aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <returns> </returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Insert aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <returns> </returns>
        bool Remove(TEntity entity);

        /// <summary>
        /// Remove aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <returns> </returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Update aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}