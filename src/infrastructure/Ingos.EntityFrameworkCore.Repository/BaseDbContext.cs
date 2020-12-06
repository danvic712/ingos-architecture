//-----------------------------------------------------------------------
// <copyright file= "BaseDbContext.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/7/4 17:57:29
// Modified by:
// Description: Base Db context
//-----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using Ingos.EntityFrameworkCore.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Ingos.EntityFrameworkCore.Repository
{
    public class BaseDbContext : DbContext, IUnitOfWork
    {
        #region Initializes

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="options">Db context options</param>
        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        {
        }

        #endregion Initializes

        #region Unit of work

        /// <summary>
        ///     Commit transaction and returns whether this operation is successful
        /// </summary>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Invoking base class methods to commit transactions
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        #endregion Unit of work
    }
}