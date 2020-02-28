//-----------------------------------------------------------------------
// <copyright file= "IRepository.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 22:24:00
// Modified by:
// Description: Generic base repository interface
//-----------------------------------------------------------------------
using Ingos.Domain.Abstractions;
using Ingos.Domain.Abstractions.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Ingos.Infrastructure.Core.EntityFrameworkCore.Interfaces
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : EntityBase<TPrimaryKey>, IAggregateRoot
    {
        /// <summary>
        /// Unit of work object
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Delete aggreate root entity by primary key
        /// </summary>
        /// <param name="id"> The primary key of this aggreate root entity </param>
        /// <returns> </returns>
        bool Delete(TPrimaryKey id);

        /// <summary>
        /// Delete aggreate root entity by primary key
        /// </summary>
        /// <param name="id"> The primary key of this aggreate root entity </param>
        /// <param name="cancellationToken"> Async task cancel token </param>
        /// <returns> </returns>
        Task<bool> DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get aggreate root entity by primary key
        /// </summary>
        /// <param name="id"> The primary key of this aggreate root entity </param>
        /// <returns> </returns>
        TEntity GetEntityById(TPrimaryKey id);

        /// <summary>
        /// Get aggreate root entity by primary key
        /// </summary>
        /// <param name="id"> The primary key of this aggreate root entity </param>
        /// <param name="cancellationToken"> Async task cancel token </param>
        /// <returns> </returns>
        Task<TEntity> GetEntityByIdAsync(TPrimaryKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Insert aggreate root entity
        /// </summary>
        /// <param name="entity"> The aggreate root entity </param>
        /// <returns> </returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Insert aggreate root entity
        /// </summary>
        /// <param name="entity"> The aggreate root entity </param>
        /// <param name="cancellationToken"> Async task cancel token </param>
        /// <returns> </returns>
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove aggreate root entity
        /// </summary>
        /// <param name="entity"> The aggreate root entity </param>
        /// <returns> </returns>
        bool Remove(TEntity entity);

        /// <summary>
        /// Remove aggreate root entity
        /// </summary>
        /// <param name="entity"> The aggreate root entity </param>
        /// <param name="cancellationToken"> Async task cancel token </param>
        /// <returns> </returns>
        Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update aggreate root entity
        /// </summary>
        /// <param name="entity"> The aggreate root entity </param>
        /// <returns> </returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Update aggreate root entity
        /// </summary>
        /// <param name="entity"> The aggreate root entity </param>
        /// <param name="cancellationToken"> Async task cancel token </param>
        /// <returns> </returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}