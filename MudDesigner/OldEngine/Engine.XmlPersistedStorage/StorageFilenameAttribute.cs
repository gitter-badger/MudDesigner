//-----------------------------------------------------------------------
// <copyright file="StorageFilenameAttribute.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace Engine.XmlPersistedStorage
{
    /// <summary>
    /// Any property decorated with a FilenameAttribute will have the property value used as the objects
    /// file name when stored using an IPersistedStorage object that stores objects to disk.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class StorageFilenameAttribute : Attribute
    {
        // Empty by design.
    }
}
