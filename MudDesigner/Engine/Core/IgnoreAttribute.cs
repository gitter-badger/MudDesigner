//-----------------------------------------------------------------------
// <copyright file="IgnoreAttribute.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// Indicates that the designated property needs to be ignored when its owning class is being persisted to storage.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute : Attribute
    {
    }
}
