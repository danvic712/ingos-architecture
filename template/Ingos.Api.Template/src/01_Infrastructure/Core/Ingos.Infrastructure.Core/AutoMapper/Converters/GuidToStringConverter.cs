//-----------------------------------------------------------------------
// <copyright file= "GuidToStringConverter.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/3/8 20:18:16
// Modified by:
// Description: Convert guid to string
//-----------------------------------------------------------------------
using AutoMapper;
using System;

namespace Ingos.Infrastructure.Core.AutoMapper.Converters
{
    /// <summary>
    /// Convert guid to string
    /// </summary>
    public class GuidToStringConverter : IValueConverter<Guid, string>
    {
        public string Convert(Guid sourceMember, ResolutionContext context)
            => sourceMember.ToString();
    }
}