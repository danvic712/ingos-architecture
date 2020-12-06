//-----------------------------------------------------------------------
// <copyright file= "IDatabaseTransaction.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/4/18 13:09:37
// Modified by:
// Description: Database transaction interface
//-----------------------------------------------------------------------
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ingos.Domain.Persistence.EntityFrameworkCore.Contracts
{
    public interface IDatabaseTransaction
    {
        /// <summary>
        /// Check current whether contain active transactions
        /// </summary>
        bool HasActiveTransaction { get; }

        /// <summary>
        /// Begin transaction
        /// </summary>
        /// <returns></returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// <param name="transaction">The instance of database transaction</param>
        /// <returns></returns>
        Task CommitTransactionAsync(IDbContextTransaction transaction);

        /// <summary>
        /// Get current transaction instance
        /// </summary>
        IDbContextTransaction GetCurrentTransaction();

        /// <summary>
        /// Rollback transaction
        /// </summary>
        void RollbackTransaction();
    }
}