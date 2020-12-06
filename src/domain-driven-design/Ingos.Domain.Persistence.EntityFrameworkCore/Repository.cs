//-----------------------------------------------------------------------
// <copyright file= "Repository.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/4/18 13:16:06
// Modified by:
// Description: Generic base repository interface implementation abstract class
//-----------------------------------------------------------------------
using System.Threading;
using System.Threading.Tasks;
using Ingos.Domain.Abstractions;
using Ingos.Domain.Abstractions.Contracts;
using Ingos.Domain.Persistence.EntityFrameworkCore.Contracts;

namespace Ingos.Domain.Persistence.EntityFrameworkCore
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity, long>
        where TEntity : EntityBase, IAggregateRoot where TContext : BaseDbContext
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dbContext">Db context object</param>
        protected Repository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Unit of work object
        /// </summary>
        public IUnitOfWork UnitOfWork => _dbContext;

        /// <summary>
        /// Db context object
        /// </summary>
        protected virtual TContext _dbContext { get; set; }

        /// <summary>
        /// Delete aggregate root entity by primary key
        /// </summary>
        /// <param name="id"> The primary key of this aggregate root entity </param>
        /// <returns> </returns>
        public virtual bool Delete(long id)
        {
            // Get entity by primary key
            var entity = _dbContext.Find<TEntity>(id);
            if (entity == null)
                return false;

            // Remove entity
            _dbContext.Remove(entity);

            return true;
        }

        /// <summary>
        /// Delete aggregate root entity by primary key
        /// </summary>
        /// <param name="id">The primary key of this aggregate root entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public virtual async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            // Get entity by primary key
            var entity = await _dbContext.FindAsync<TEntity>(id, cancellationToken);
            if (entity == null)
                return false;

            // Remove entity
            _dbContext.Remove(entity);

            return true;
        }

        /// <summary>
        /// Get aggregate root entity by primary key
        /// </summary>
        /// <param name="id">The primary key of this aggregate root entity</param>
        /// <returns> </returns>
        public virtual TEntity GetEntityById(long id)
        {
            return _dbContext.Find<TEntity>(id);
        }

        /// <summary>
        /// Get aggregate root entity by primary key
        /// </summary>
        /// <param name="id">The primary key of this aggregate root entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public virtual async Task<TEntity> GetEntityByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.FindAsync<TEntity>(id, cancellationToken);
        }

        /// <summary>
        /// Insert aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <returns> </returns>
        public TEntity Insert(TEntity entity)
        {
            return _dbContext.Add(entity).Entity;
        }

        /// <summary>
        /// Insert aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Insert(entity));
        }

        /// <summary>
        /// Remove aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <returns> </returns>
        public virtual bool Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
            return true;
        }

        /// <summary>
        /// Remove aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public virtual async Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Remove(entity));
        }

        /// <summary>
        /// Update aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <returns> </returns>
        public virtual TEntity Update(TEntity entity)
        {
            return _dbContext.Update(entity).Entity;
        }

        /// <summary>
        /// Update aggregate root entity
        /// </summary>
        /// <param name="entity">The aggregate root entity</param>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Update(entity));
        }
    }
}