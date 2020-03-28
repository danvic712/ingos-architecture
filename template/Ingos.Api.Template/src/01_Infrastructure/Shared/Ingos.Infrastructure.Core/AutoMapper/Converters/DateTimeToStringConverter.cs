//-----------------------------------------------------------------------
// <copyright file= "DateTimeToStringConverter.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/3/8 20:16:34
// Modified by:
// Description: Convert datetime to string with yyyy-MM-dd HH:mm:ss format
//-----------------------------------------------------------------------
using AutoMapper;
using System;

namespace Ingos.Infrastructure.Core.AutoMapper.Converters
{
    /// <summary>
    /// Convert datetime to string with yyyy-MM-dd HH:mm:ss format
    /// </summary>
    public class DateTimeToStringConverter : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime source, ResolutionContext context)
            => source.ToString("yyyy-MM-dd HH:mm:ss");
    }
}