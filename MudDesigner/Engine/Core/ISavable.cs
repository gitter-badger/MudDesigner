/* ISavable
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Provides methods for saving game objects. Most game objects can be loaded after saving with no special requirements, however any
 *                   object that contains a circular referencing property, must have a JSonProperty(ReferenceLoopHandling = Serialize) attribute applied
 *                   to the offending circular referenced property.
 */

//Microsoft .NET using statements
using System;
using System.IO;

//Abstract.Actions namespace is used for Types that will perform action based code. Such as saving, loading or being used.
namespace MudDesigner.Engine.Core
{    
    /// <summary>
    /// Provides methods for saving game objects. Most game objects can be loaded after saving with no special requirements, however any
    /// object that contains a circular referencing property, must have a JSonProperty(ReferenceLoopHandling = Serialize) attribute applied
    /// to the offending circular referenced property.
    /// </summary>
    public interface ISavable
    {
        /// Provides methods for saving game objects. Most game objects can be saved with no special requirements, however any
        /// object that contains a circular referencing property, must have a JSonProperty(ReferenceLoopHandling = Serialize) attribute applied
        /// to the offending circular referenced property.
        /// </summary>
        /// <param name="objectToSave">A reference to an object that needs to be wrote to file</param>
        /// <param name="fullFilePath">The full path including filename that this file needs to be saved</param>
        void Save(object objectToSave, string fullFilePath);
    }
}