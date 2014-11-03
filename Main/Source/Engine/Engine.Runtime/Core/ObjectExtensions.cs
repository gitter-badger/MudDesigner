//-----------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Runtime.Core
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Extension methods for the Object class.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the name of the property provided.
        /// Provides a mechanism to avoid literal strings used when accessing object members by name.
        /// </summary>
        /// <typeparam name="T">The type whose member you want to access.</typeparam>
        /// <param name="obj">The parameters.</param>
        /// <param name="expr">The expression.</param>
        /// <returns>
        /// Returns the name of the property provided.
        /// </returns>
        public static string GetPropertyName<T>(this T obj, Expression<Func<T, object>> expr)
        {
            var member = expr.Body as MemberExpression;
            var unary = expr.Body as UnaryExpression;
            var memberExpression = member ?? (unary != null ? unary.Operand as MemberExpression : null);

            if (memberExpression == null)
            {
                return null;
            }

            return memberExpression.Member.Name;
        }
    }
}
