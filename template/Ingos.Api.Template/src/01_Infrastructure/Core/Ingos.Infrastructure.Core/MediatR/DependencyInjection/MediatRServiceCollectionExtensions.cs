//-----------------------------------------------------------------------
// <copyright file= "MediatRServiceCollectionExtensions.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/2/28 22:43:08
// Modified by:
// Description: Ingos MediatR custom service injection extension method
//-----------------------------------------------------------------------
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ingos.Infrastructure.Core.MediatR.DependencyInjection
{
    public static class MediatRServiceCollectionExtensions
    {
        /// <summary>
        /// Inject MediatR into IServiceCollection
        /// </summary>
        /// <param name="services">The instance of <see cref="IServiceCollection"/></param>
        /// <returns></returns>
        public static IServiceCollection AddIngosApplicationMediatR(this IServiceCollection services,
            Action<IngosMediatRDescriptionOptions> setupAction)
        {
            if (setupAction == null)
                throw new ArgumentNullException(nameof(setupAction));

            // Get mediatr config options
            //
            var options = new IngosMediatRDescriptionOptions();
            setupAction?.Invoke(options);

            return AddMediatRService(services, options);
        }

        /// <summary>
        /// Add MediatR
        /// </summary>
        /// <param name="services">The collection of services</param>
        /// <param name="options">The mediatr config options</param>
        /// <returns></returns>
        public static IServiceCollection AddMediatRService(IServiceCollection services, IngosMediatRDescriptionOptions options)
        {
            // Get Startup's type
            var mediators = new List<Type> { options.StartupClassType };

            // The base request interface's type
            var parentRequestType = typeof(IRequest<>);

            // The base notification interface's type
            var parentNotificationType = typeof(INotification);

            foreach (var item in options.Assemblies)
            {
                var instances = Assembly.Load(item).GetTypes();

                foreach (var instance in instances)
                {
                    // Get the interfaces info
                    //
                    var baseInterfaces = instance.GetInterfaces();
                    if (baseInterfaces.Count() == 0 || !baseInterfaces.Any())
                        continue;

                    // Get all class which inheritance the IRequest<T> interface
                    //
                    var requestTypes = baseInterfaces.Where(i => i.IsGenericType
                        && i.GetGenericTypeDefinition() == parentRequestType);

                    if (requestTypes.Count() != 0 || requestTypes.Any())
                        mediators.Add(instance);

                    // Get all class which inheritance the INotification interface
                    //
                    var notificationTypes = baseInterfaces.Where(i => i.FullName == parentNotificationType.FullName);

                    if (notificationTypes.Count() != 0 || notificationTypes.Any())
                        mediators.Add(instance);
                }
            }

            // Add MediatR
            services.AddMediatR(mediators.ToArray());

            return services;
        }
    }
}