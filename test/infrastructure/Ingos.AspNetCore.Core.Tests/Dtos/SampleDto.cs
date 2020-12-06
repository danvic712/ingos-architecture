//-----------------------------------------------------------------------
// <copyright file= "SampleDto.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/12/5 21:40:16
// Modified by:
// Description: Test sample data transfer object
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Ingos.AspNetCore.Core.Tests.Dtos
{
    public class SampleDto
    {
        [Required] public long Id { get; set; }

        public DateTime DateTime { get; set; }
    }
}