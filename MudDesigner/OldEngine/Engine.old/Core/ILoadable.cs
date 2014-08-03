//-----------------------------------------------------------------------
// <copyright file="ILoadable.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.IO;

namespace MudDesigner.Engine.Core
{
    /// <summary>
    /// Provides methods for saving and loading game objects.Most game objects can be loaded after saving with no special requirements
    /// object that contains a circular referencing property, must have a JSonProperty(ReferenceLoopHandling = Serialize) attribute applied
    /// to the offending circular referenced property.
    /// </summary>
    public interface ILoadable
    {
        /// <summary>
        /// Provides a method for loading game objects. Most game objects can be loaded after saving with no special requirements, however any
        /// object that contains a circular referencing property, must have a JSonProperty(ReferenceLoopHandling = Serialize) attribute applied
        /// to the offending circular referenced property.
        /// </summary>
        /// <param name="fullFilePath">The full path including filename that this file needs to be saved</param>
        /// <param name="t">The Type that needs to be instanced and restored.</param>
        /// <returns></returns>
        object Load(string fullFilePath, Type t);
    }
}