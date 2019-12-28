//-----------------------------------------------------------------------
// <copyright file= "GuidToStringConverter.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2019/12/28 15:52:22
// Modified by:
// Description: Convert guid to string
//-----------------------------------------------------------------------
using AutoMapper;
using System;

namespace Ingos.AutoMapper.Converters
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