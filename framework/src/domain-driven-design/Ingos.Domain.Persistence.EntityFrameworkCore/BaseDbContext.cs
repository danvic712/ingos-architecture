//-----------------------------------------------------------------------
// <copyright file= "BaseDbContext.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/4/18 13:17:31
// Modified by:
// Description: Ingos base database context object
//-----------------------------------------------------------------------
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Ingos.Domain.Persistence.EntityFrameworkCore.Contracts;
using Ingos.Domain.Persistence.EntityFrameworkCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ingos.Domain.Persistence.EntityFrameworkCore
{
    public class BaseDbContext : DbContext, IUnitOfWork, IDatabaseTransaction
    {
        #region Initialize

        /// <summary>
        /// The instance of MediatR
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options">Db context options</param>
        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options">Db context options</param>
        /// <param name="mediator">The instance of MediatR</param>
        public BaseDbContext(DbContextOptions options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion Initialize

        #region Unit of work

        /// <summary>
        /// Commit transaction and returns whether this operation is successful
        /// </summary>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch Domain Events
            await _mediator.DispatchDomainEventsAsync(this);

            // Invoking base class methods to commit transactions
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        #endregion Unit of work

        #region Transaction

        /// <summary>
        /// The current transaction instance
        /// </summary>
        private IDbContextTransaction _currentTransaction;

        /// <summary>
        /// Check current whether contain active transactions
        /// </summary>
        public bool HasActiveTransaction => _currentTransaction != null;

        /// <summary>
        /// Begin transaction
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            // Check whether has transaction
            if (_currentTransaction != null)
                return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// <param name="transaction">The instance of database transaction</param>
        /// <returns> </returns>
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            if (transaction != _currentTransaction)
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        /// <summary>
        /// Get current transaction instance
        /// </summary>
        /// <returns> </returns>
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        /// <summary>
        /// Rollback transaction
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        #endregion Transaction
    }
}