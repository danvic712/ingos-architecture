//-----------------------------------------------------------------------
// <copyright file= "IDatabaseTransaction.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 22:21:56
// Modified by:
// Description: Database transaction interface
//-----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Ingos.Infrastructure.Core.EntityFrameworkCore.Interfaces
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
        /// <returns> </returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// <param name="transaction"> The instance of database transaction </param>
        /// <returns> </returns>
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