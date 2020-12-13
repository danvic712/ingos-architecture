//-----------------------------------------------------------------------
// <copyright file= "BaseRepository.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/6/3 20:50:36
// Modified by:
// Description: Base generic data access repository interface implementation
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ingos.EntityFrameworkCore.Repository.Contracts;

namespace Ingos.EntityFrameworkCore.Repository
{
    public abstract class BaseRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey>
        where TEntity : class
    {
        #region Initializes

        /// <summary>
        ///     Db context object
        /// </summary>
        protected virtual IngosDbContext DbContext { get; set; }

        /// <summary>
        ///     Unit of work object
        /// </summary>
        public IUnitOfWork UnitOfWork => DbContext;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="context">Db context object</param>
        protected BaseRepository(IngosDbContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Initializes

        #region Services

        /// <summary>
        ///     Delete entity by primary key
        /// </summary>
        /// <param name="id">The primary key of this entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public async Task<bool> DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            // Get entity by primary key
            var entity = await DbContext.FindAsync<TEntity>(id, cancellationToken);
            if (entity == null)
                return false;

            // Remove entity
            DbContext.Remove(entity);

            return true;
        }

        /// <summary>
        ///     Get entity by primary key
        /// </summary>
        /// <param name="id">The primary key of this entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public async Task<TEntity> GetEntityByIdAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            return await DbContext.FindAsync<TEntity>(id, cancellationToken);
        }

        /// <summary>
        ///     Insert entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entry = await DbContext.AddAsync(entity, cancellationToken);
            return entry.Entity;
        }

        /// <summary>
        ///     Insert entities
        /// </summary>
        /// <param name="entities">The entity collection</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public async Task<bool> InsertAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
        {
            // Check whether this entity collection has data
            //
            var elements = entities.Count;
            if (!entities.Any() || elements == 0)
                return false;

            // Save entities
            //
            await DbContext.AddRangeAsync(entities, cancellationToken);
            var count = await DbContext.SaveChangesAsync(cancellationToken);

            return count == elements;
        }

        /// <summary>
        ///     Remove entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public async Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            // Check whether this entity is null or not
            if (entity == null)
                return false;

            // Remove this entity
            DbContext.Remove(entity);

            return await Task.FromResult(true);
        }

        /// <summary>
        ///     Remove entity
        /// </summary>
        /// <param name="entities">The entity collection</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public async Task<bool> RemoveAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
        {
            // Check whether this entity is null or not
            if (entities == null || !entities.Any())
                return false;

            // Remove this entity
            DbContext.Remove(entities);

            return await Task.FromResult(true);
        }

        /// <summary>
        ///     Update entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entry = DbContext.Update(entity);
            return await Task.FromResult(entry.Entity);
        }

        #endregion Services
    }

    public abstract class BaseRepository<TEntity> : BaseRepository<TEntity, Guid>
        where TEntity : class
    {
        protected BaseRepository(IngosDbContext context) : base(context)
        {
        }
    }
}