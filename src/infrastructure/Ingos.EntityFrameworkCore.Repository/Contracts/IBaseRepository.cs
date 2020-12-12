//-----------------------------------------------------------------------
// <copyright file= "IBaseRepository.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/6/3 20:50:13
// Modified by:
// Description: Base generic data access repository interface
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ingos.EntityFrameworkCore.Repository.Models;

namespace Ingos.EntityFrameworkCore.Repository.Contracts
{
    public interface IBaseRepository : IDisposable
    {
        /// <summary>
        ///     Unit of work object
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }

    public interface IBaseRepository<TEntity> : IBaseRepository
        where TEntity : class
    {
        /// <summary>
        ///     Insert entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Insert entities
        /// </summary>
        /// <param name="entities">The entity collection</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns></returns>
        Task<bool> InsertAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Remove entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Remove entity
        /// </summary>
        /// <param name="entities">The entity collection</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<bool> RemoveAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Update entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }

    public interface IBaseRepository<TEntity, in TPrimaryKey> : IBaseRepository<TEntity>
        where TEntity : EntityBase<TPrimaryKey>
    {
        /// <summary>
        ///     Get entity by primary key
        /// </summary>
        /// <param name="id">The primary key of this entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<TEntity> GetEntityByIdAsync(TPrimaryKey id, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Delete entity by primary key
        /// </summary>
        /// <param name="id">The primary key of this entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<bool> DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default);
    }
}