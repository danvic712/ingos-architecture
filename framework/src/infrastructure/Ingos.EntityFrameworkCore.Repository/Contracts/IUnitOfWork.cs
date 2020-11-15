//-----------------------------------------------------------------------
// <copyright file= "IUnitOfWork.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/7/4 17:53:47
// Modified by:
// Description: The unit of work interface
//-----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

namespace Ingos.EntityFrameworkCore.Repository.Contracts
{
    public interface IUnitOfWork
    {
        /// <summary>
        ///     Commit transaction and returns the number of affected rows
        /// </summary>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Commit transaction and returns whether this operation is successful
        /// </summary>
        /// <param name="cancellationToken">Async task cancel token</param>
        /// <returns> </returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}