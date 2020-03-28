//-----------------------------------------------------------------------
// <copyright file= "ValueObjectBase.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2020/3/28 19:28:32
// Modified by:
// Description: Generic base value object class
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;

namespace Ingos.Domain.Abstractions
{
    public abstract class ValueObjectBase
    {
        /// <summary>
        /// Get value object's atomic values
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetAtomicValues();

        /// <summary>
        /// Determine if two value objects are the same
        /// </summary>
        /// <param name="obj">The value object of judgment</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            ValueObjectBase other = (ValueObjectBase)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (thisValues.Current is null ^ otherValues.Current is null)
                {
                    return false;
                }
                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        /// <summary>
        /// Get the value object's hash value
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => GetAtomicValues().Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);

        /// <summary>
        /// Get current value object's copy
        /// </summary>
        /// <returns></returns>
        public ValueObjectBase GetCopy() => MemberwiseClone() as ValueObjectBase;

        /// <summary>
        /// Determine the two value object they are equal
        /// </summary>
        /// <param name="left">The first value object to judge</param>
        /// <param name="right">The second value object to judge</param>
        /// <returns></returns>
        protected static bool EqualOperator(ValueObjectBase left, ValueObjectBase right)
        {
            if (left is null ^ right is null)
                return false;

            return left is null || left.Equals(right);
        }

        /// <summary>
        /// Determine the two value object they are not equal
        /// </summary>
        /// <param name="left">The first value object to judge</param>
        /// <param name="right">The second value object to judge</param>
        /// <returns></returns>
        protected static bool NotEqualOperator(ValueObjectBase left, ValueObjectBase right) => !(EqualOperator(left, right));
    }
}