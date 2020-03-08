//-----------------------------------------------------------------------
// <copyright file= "DateToStringConverter.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/3/8 20:17:34
// Modified by:
// Description: Convert date to string with yyyy-MM-dd format
//-----------------------------------------------------------------------
using AutoMapper;
using System;

namespace Ingos.Infrastructure.Core.AutoMapper.Converters
{
    /// <summary>
    /// Convert date to string with yyyy-MM-dd format
    /// </summary>
    public class DateToStringConverter : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime source, ResolutionContext context)
            => source.ToString("yyyy-MM-dd");
    }
}