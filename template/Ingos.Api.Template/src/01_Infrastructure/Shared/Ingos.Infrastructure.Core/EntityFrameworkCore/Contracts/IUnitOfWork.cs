//-----------------------------------------------------------------------
// <copyright file= "IUnitOfWork.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 22:20:03
// Modified by:
// Description: The unit of work interface
//-----------------------------------------------------------------------
using System.Threading;
using System.Threading.Tasks;

namespace Ingos.Infrastructure.Core.EntityFrameworkCore.Contracts
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commit transaction and returns the number of affected rows
        /// </summary>
        /// <param name="cancellationToken"> Async task cancel token </param>
        /// <returns> </returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commit transaction and returns whether this operation is successful
        /// </summary>
        /// <param name="cancellationToken"> Async task cancel token </param>
        /// <returns> </returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}