//-----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Runtime.CompilerServices;

namespace MudEngine.Engine.Core
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Gets the name of the property provided in the parameter.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="member">The member.</param>
        /// <returns></returns>
        public static string GetPropertyName(this object obj, [CallerMemberName] string member = "")
        {
            return member;
        }
    }
}
