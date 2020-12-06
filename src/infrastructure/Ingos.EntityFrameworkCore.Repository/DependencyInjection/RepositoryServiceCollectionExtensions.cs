//-----------------------------------------------------------------------
// <copyright file= "RepositoryServiceCollectionExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/7/6 20:23:51
// Modified by:
// Description: Services container injection extension method
//-----------------------------------------------------------------------

using Ingos.EntityFrameworkCore.Repository;
using Ingos.EntityFrameworkCore.Repository.Contracts;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoryServiceCollectionExtensions
    {
        #region Sevices

        /// <summary>
        ///     Add data access repository service
        /// </summary>
        /// <param name="services">Services container</param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            return services;
        }

        #endregion Sevices
    }
}