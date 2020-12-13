//-----------------------------------------------------------------------
// <copyright file= "IngosDbContext.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/7/4 17:57:29
// Modified by:
// Description: Base Db context
//-----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using Ingos.EntityFrameworkCore.Repository.ChangeTracking;
using Ingos.EntityFrameworkCore.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ingos.EntityFrameworkCore.Repository
{
    public class IngosDbContext : DbContext, IUnitOfWork
    {
        /// <summary>
        ///     Logger factory instance
        /// </summary>
        public static readonly ILoggerFactory EFCoreLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                .AddConsole();
        });

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="options">Db context options</param>
        public IngosDbContext(DbContextOptions<IngosDbContext> options)
            : base(options)
        {
        }

        public DbSet<Audit> Audits { get; set; }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(EFCoreLoggerFactory);
        }
    }
}