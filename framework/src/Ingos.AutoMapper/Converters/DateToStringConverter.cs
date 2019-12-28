//-----------------------------------------------------------------------
// <copyright file= "DateToStringConverter.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2019/12/28 15:42:01
// Modified by:
// Description: Convert date to string with yyyy-MM-dd format
//-----------------------------------------------------------------------
using AutoMapper;
using System;

namespace Ingos.AutoMapper.Converters
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